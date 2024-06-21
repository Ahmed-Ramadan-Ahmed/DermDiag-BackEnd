using DermDiag.DTO;
using DermDiag.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using DermDiag.Repository;
using DermDiag.Migrations;

namespace DermDiag.Repository
{
    public class PatientRepository
    {
        private readonly DermDiagContext context1;
        //private static readonly MeetingRepository _MeetingRepository = new MeetingRepository();
        public PatientRepository(DermDiagContext context)
        {
            context1 = context;
            //_MeetingRepository = meeting_repository;
        }

        /*################################## GET ALL DOCTORS ##################################*/

        public List<DoctorHomeDTO> GetAllDoctors(int Id)
        {
            var patient = context1.Patients.Include(p => p.Doctors).FirstOrDefault(p => p.Id == Id);

            if (patient == null)
            {
                throw new Exception("Patient Not Found!");
            }

            var docotrs = context1.Doctors.ToList();
            
            List<DoctorHomeDTO> ReturnDoctors = new List<DoctorHomeDTO>();
            foreach (var doctor in docotrs)
            {
                ReturnDoctors.Add(new DoctorHomeDTO()
                {
                    Image = doctor.Image,
                    Id = doctor.Id,
                    Name = doctor.Name,
                    Rating = doctor.Rating,
                    IsFavourite = patient.Doctors.Any(d => d.Id == doctor.Id) ? true : false,
                });
            }
            return ReturnDoctors;
        }


        /*################################## SEARCH FOR DOCTORS ##################################*/

        public List<DoctorHomeDTO> SearchDoctorsByName(string doctorName, int PId)
        {
            var doctors = context1.Doctors
                .Where(d => d.Name.StartsWith(doctorName))
                .ToList();

            var patient = context1.Patients
                .Include(p => p.Doctors)
                .FirstOrDefault(p => p.Id == PId);

            if (patient == null)
            {
                throw new Exception("Patient not found!");
            }

            List<DoctorHomeDTO> returnDoctors = new List<DoctorHomeDTO>();
            foreach (var doctor in doctors)
            {
                bool isFavourite = patient.Doctors.Any(d => d.Id == doctor.Id);
                returnDoctors.Add(new DoctorHomeDTO()
                {
                    Image = doctor.Image,
                    Id = doctor.Id,
                    Name = doctor.Name,
                    Rating = doctor.Rating,
                    IsFavourite = isFavourite
                });
            }
            return returnDoctors;
        }

        /*################################## GET FAVORITE DOCTORS ##################################*/

        public List<DoctorHomeDTO> FavoriteDoctors(int Id)
        {
            var patient = context1.Patients.Include(p => p.Doctors).FirstOrDefault(p => p.Id == Id);

            if (patient == null)
            {
                throw new Exception("Not Found!");
            }

            var favoriteDoctors = patient.Doctors.Select(doctor => new DoctorHomeDTO
            {
                Image = doctor.Image,
                Id = doctor.Id,
                Name = doctor.Name,
                Rating = doctor.Rating,
                IsFavourite = true
            }).ToList();

            return favoriteDoctors;
        }

        /*################################## ADD FAVORITE DOCTORS ##################################*/

        public bool AddFavoriteDoctors(int patientId, int doctorId)
        {
            try
            {
                var patient = context1.Patients.FirstOrDefault(p => p.Id == patientId);
                if (patient == null)
                {
                    return false;
                }

                var doctor = context1.Doctors.FirstOrDefault(d => d.Id == doctorId);
                if (doctor == null)
                {
                    return false;
                }

                if (patient.Doctors.Any(d => d.Id == doctorId))
                {
                    return false;
                }

                patient.Doctors.Add(doctor);
                context1.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /*################################## REMOVE FAVORITE DOCTORS ##################################*/

        public bool RemoveFavoriteDoctors(int patientId, int doctorId)
        {
            try
            {
                var patient = context1.Patients.Include(p => p.Doctors).FirstOrDefault(p => p.Id == patientId);
                if (patient == null)
                {
                    return false;
                }

                var doctor = context1.Doctors.FirstOrDefault(d => d.Id == doctorId);
                if (doctor == null)
                {
                    return false;
                }

                if (!patient.Doctors.Any(d => d.Id == doctorId))
                {
                    return false;
                }

                patient.Doctors.Remove(doctor);
                context1.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /*################################## Update Profile ##################################*/

        public bool UpdatePatientProfile(UpdatePatientDTO P1, int patientId)
        {
            try
            {
                var patient = context1.Patients.FirstOrDefault(p => p.Id == patientId);
                if (patient == null)
                {
                    return false; // Patient not found
                }



                // Update all fields except email
                patient.Name = P1.Name;
                patient.Email = P1.Email;
                patient.Phone = P1.Phone;
                patient.Password = P1.Password;
                patient.Image = P1.Image;
                patient.Address = P1.Address;


                // Save changes to the database
                context1.Patients.Update(patient);
                context1.SaveChanges();

                return true; // Successfully updated
            }
            catch (Exception ex)
            {

                return false; // Update failed
            }
        }


        /*################################## GET Medicine List ##################################*/

        public List<TreatmentPlanDTO> GetTreatmentPlan(int doctorId, int patientId)
        {
            try
            {
                var patient = context1.Patients.FirstOrDefault(p => p.Id == patientId);
                if (patient == null)
                {
                    throw new NullReferenceException("Patient not found!");
                }

                var doctor = context1.Doctors.FirstOrDefault(p => p.Id == doctorId);
                if (doctor == null)
                {
                    throw new NullReferenceException("Doctor not found!");
                }
                var medicine = new List<TreatmentPlanDTO>();
                var treatmentPlans = context1.MedicineAdvices.Where(m => m.DoctorId == doctorId && m.PatientId == patientId).ToList();
                foreach (var treatmentPlan in treatmentPlans)
                {
                    var TreatmentPlans = new TreatmentPlanDTO()
                    {
                        MedicineName = treatmentPlan.MedicineName,
                        Quantity = treatmentPlan.Quantity,
                        Frequency = treatmentPlan.Frequency
                    };
                    medicine.Add(TreatmentPlans);


                }
                return medicine;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*################################## ADD TASKS ##################################*/
        public void AddTask(int p_Id, TaskDTO task)
        {
            try
            {
                var patient = context1.Patients.FirstOrDefault(p => p.Id == p_Id);
                if (patient == null)
                {
                    throw new NullReferenceException("Patient not found!");
                }

                var newTask = new Tasks()
                {
                    PatientId = p_Id,
                    Date = task.Date,
                    Title = task.Title,
                    Note = task.Note,
                    Starttime = task.Starttime,
                    Endtime = task.Endtime,
                    RepeatingDays = task.RepeatingDays
                };

                context1.Tasks.Add(newTask);
                context1.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*################################## Delete TASKS ##################################*/
        public void DeleteTask(int p_Id, int taskId)
        {
            try
            {
                var patient = context1.Patients.FirstOrDefault(p => p.Id == p_Id);
                if (patient == null)
                {
                    throw new NullReferenceException("Patient not found!");
                }

                var task = context1.Tasks.FirstOrDefault(t => t.Id == taskId && t.PatientId == p_Id);
                if (task == null)
                {
                    throw new NullReferenceException("Task not found!");
                }

                context1.Tasks.Remove(task);
                context1.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*################################## GET TASKS ##################################*/
        public List<TaskDTO> GetTasks(int patientId)
        {
            try
            {
                var patient = context1.Patients.FirstOrDefault(p => p.Id == patientId);
                if (patient == null)
                {
                    throw new NullReferenceException("Patient not found!");
                }

                // Query tasks related to the patient
                var tasks = context1.Tasks
                    .Where(t => t.PatientId == patientId)
                    .Select(t => new TaskDTO
                    {
                        Date = t.Date,
                        Title = t.Title,
                        Note = t.Note,
                        Starttime = t.Starttime,
                        Endtime = t.Endtime,
                        RepeatingDays = t.RepeatingDays
                    })
                    .ToList();

                return tasks;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /*################################## Add Review ##################################*/
        public void AddReview(int doctorId, int patientId, ReviewDto reviewDto)
        {
            try
            {
                var patient = context1.Patients.FirstOrDefault(p => p.Id == patientId);
                if (patient == null)
                {
                    throw new Exception("Patient not found!");
                }

                var doctor = context1.Doctors.FirstOrDefault(d => d.Id == doctorId);
                if (doctor == null)
                {
                    throw new Exception("Doctor not found!");
                }

                var review = new Review
                {
                    Feedback = reviewDto.Feedback,
                    Rate = reviewDto.Rate,
                    PatientId = patientId,
                    DoctorId = doctorId,

                };

                context1.Reviews.Add(review);
                context1.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /*################################## Update Review ##################################*/

        public bool UpdateReview(int doctorId, int patientId, ReviewDto reviewDto)
        {
            try
            {
                var patient = context1.Patients.FirstOrDefault(p => p.Id == patientId);
                if (patient == null)
                {
                    throw new Exception("Patient not found!");
                }

                var doctor = context1.Doctors.FirstOrDefault(d => d.Id == doctorId);
                if (doctor == null)
                {
                    throw new Exception("Doctor not found!");
                }
                var review = new Review
                {
                    Feedback = reviewDto.Feedback,
                    Rate = reviewDto.Rate,
                    PatientId = patientId,
                    DoctorId = doctorId,

                };


                context1.Reviews.Update(review);
                context1.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*################################## Delete Review ##################################*/

        public bool DeleteReview(int doctorId, int patientId)
        {
            try
            {
                var patient = context1.Patients.FirstOrDefault(p => p.Id == patientId);
                if (patient == null)
                {
                    throw new Exception("Patient not found!");
                }

                var doctor = context1.Doctors.FirstOrDefault(d => d.Id == doctorId);
                if (doctor == null)
                {
                    throw new Exception("Doctor not found!");
                }

                var review = context1.Reviews.FirstOrDefault(r => r.PatientId == patientId && r.DoctorId == doctorId);
                if (review == null)
                {
                    throw new Exception("Review not found!");
                }

                context1.Reviews.Remove(review);
                context1.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*################################## Get Review list ##################################*/

        public List<GetReviewDTO> GetAllReviews(int doctorId)
        {
            var doctor = context1.Doctors.Include(d => d.Reviews).FirstOrDefault(d => d.Id == doctorId);
            if (doctor == null)
            {
                throw new Exception("Doctor not found!");
            }

            var reviews = context1.Reviews
                .Where(r => r.DoctorId == doctorId)
                .Include(r => r.Patient) // Include the patient for additional information
                .Select(r => new GetReviewDTO
                {
                    Feedback = r.Feedback,
                    Rate = r.Rate,
                    PatientName = r.Patient.Name,
                    CurrentDate = DateTime.Now // Or r.CreatedDate if you have a creation date field
                })
                .ToList();

            return reviews;
        }

        /*################################## Book ##################################*/

        public bool CreateBook(BookDTO book)
        {
            var Patient = context1.Patients.Find(book.PatientID);
            if (Patient == null)
            {
                throw new ArgumentNullException("Patient Not Found");
            }

            var Doctor = context1.Doctors.Find(book.DoctorID);

            if (Doctor == null)
            {
                throw new ArgumentNullException("Doctor Not Found");
            }

            const int SessionTime = 1;

            var Apointments = context1.Books.Where(BOOK => BOOK.DoctorId == book.DoctorID).Select(Apointment => Apointment.AppointmentDate);

            var IsAvaibleApointment =
            Apointments.Where(Apointment =>
            Apointment != null &&
            Apointment.Value.Year == book.Date.Year &&
            Apointment.Value.Month == book.Date.Month &&
            Apointment.Value.Day == book.Date.Day &&
            (Apointment.Value.Hour == book.Date.Hour ||
            (Apointment.Value.Hour - book.Date.Hour == SessionTime && Apointment.Value.Minute < book.Date.Minute) ||
            (Apointment.Value.Hour - book.Date.Hour == -SessionTime && Apointment.Value.Minute > book.Date.Minute)
            )).Count() == 0;

            if (!IsAvaibleApointment)
            {
                throw new AccessViolationException("Apointment Not Available");
            }
            else if(book.Date < DateTime.Now.AddHours(12))
            {
                throw new AccessViolationException("Apointment can't be earlier than 12 hour from now !");
            }

            //var doctor = context1.Doctors.FirstOrDefault(d => d.Id == book.DoctorID);
           // var patient = context1.Patients.FirstOrDefault(d => d.Id == book.PatientID);
            

            Payment payment = new()
            {
                ReceiverID = book.DoctorID,
                SenderID = book.PatientID,
                Date = DateTime.Now,
                Status = "Comming"
            };
            context1.Payments.Add(payment);
            context1.SaveChanges();
            
            context1.Books.Add(
                new()
                {
                    PatientId = book.PatientID,
                    DoctorId = book.DoctorID,
                    PaymentId = payment.Id,
                    AppointmentDate = book.Date
                }
                );
            context1.SaveChanges(); 

            return true;
        }
    }

}
