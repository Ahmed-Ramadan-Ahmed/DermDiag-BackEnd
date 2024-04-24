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
                var patient = context1.Patients.Include(p=>p.Doctors).FirstOrDefault(p => p.Id == patientId);
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

        public bool UpdatePatientProfile(int patientId, string newName, string newEmail, string newPhoneNumber,string newPassword, string newImage)
        {
            try
            {
                var patient = context1.Patients.FirstOrDefault(p => p.Id == patientId);
                if (patient == null)
                {
                    return false; // Patient not found
                }

                // Update patient's information
                patient.Name = newName;
                patient.Email = newEmail;
                patient.Phone = newPhoneNumber;
                patient.Password= newPassword;
                patient.Image = newImage;

                context1.SaveChanges(); // Save changes to the database

                return true; // Successfully updated
            }
            catch (Exception ex)
            {
                // Handle exceptions, logging, etc.
                return false; // Update failed
            }
        }

    }
}
