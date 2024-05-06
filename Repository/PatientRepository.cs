using DermDiag.DTO;
using DermDiag.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace DermDiag.Repository
{
    public class PatientRepository
    {
        private readonly DermDiagContext context1;
        public PatientRepository(DermDiagContext context)
        {
            context1 = context;
        }

        /*################################## GET ALL DOCTORS ##################################*/

        public List<DoctorHomeDTO> GetAll(int Id)
        {
            var docotrs = context1.Doctors.ToList();
            var patient = context1.Patients.Include(p => p.Doctors).FirstOrDefault(p => p.Id == Id);

            if (patient == null)
            {
                throw new Exception("Not Found!");
            }

            List<DoctorHomeDTO> ReturnDoctors = new List<DoctorHomeDTO>();
            foreach (var doctor in docotrs)
            {
                ReturnDoctors.Add(new DoctorHomeDTO()
                {
                    Image = doctor.Image,
                    Id = doctor.Id,
                    Name = doctor.Name,
                    Rating = doctor.Rating,
                    IsFavourite = patient.Doctors.FirstOrDefault(d => d.Id == doctor.Id) != null ? true : false,
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

                if (P1.Email == null)
                {
                   
                    patient.Email = P1.Email;
                   
                }
               
                // Update all fields except email
                patient.Name = P1.Name;
                patient.Phone = P1.Phone;
                patient.Password = P1.Password;
                patient.Image = P1.Image;
                patient.Address = P1.Address;
                

                // Save changes to the database
                context1.Update(patient);
                context1.SaveChanges();

                return true; // Successfully updated
            }
            catch (Exception ex)
            {
                // Handle exceptions, logging, etc.
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

    }


}
