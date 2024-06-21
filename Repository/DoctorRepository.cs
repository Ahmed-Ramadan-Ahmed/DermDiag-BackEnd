using DermDiag.DTO;
using DermDiag.Models;
using Microsoft.EntityFrameworkCore;

namespace DermDiag.Repository
{
    public class DoctorRepository
    {
        private readonly DermDiagContext _context;

        public DoctorRepository(DermDiagContext context)
        {
            _context = context;
        }
        /*################################## GET ALL PATIENTS ##################################*/

        public List<PatientHomeDTO> GetAllPatients(int Id)
        {
            var Patients = _context.Books
                                .Where(book => book.DoctorId == Id)
                                .Select(book => new PatientHomeDTO
                                {
                                    Id = book.PatientId,
                                    AppointmentDate = book.AppointmentDate,
                                    Name = _context.Patients.FirstOrDefault(P => P.Id == book.PatientId).Name,
                                    Image = _context.Patients.FirstOrDefault(P => P.Id == book.PatientId).Image
                                })
                                .ToList();

            if(Patients == null)
            {
                throw new Exception("No patients associated with this doctor!");
            }

            return Patients;
        }

        /*################################## SEARCH FOR PATIENTS ##################################*/

        public List<PatientHomeDTO> SearchPatientsByName(string patientName)
        {
            var patients = _context.Patients
                .Where(p => p.Name.StartsWith(patientName))
                .Select(p => new PatientHomeDTO
                {
                    Image = p.Image,
                    Name = p.Name
                    // Appointment date
                })
                .ToList();

            return patients;
        }
        /*################################## Update Profile ##################################*/

        public bool UpdateDoctorProfile(UpdateDoctorDtO d1,int doctortId)
        {
            try
            {
                var doctor = _context.Doctors.FirstOrDefault(p => p.Id == doctortId);
                if (doctor == null)
                {
                    return false; 
                }


                doctor.Name = d1.Name;
                doctor.Email = d1.Email;
                doctor.Phone = d1.Phone;
                doctor.Password = d1.Password;
                doctor.Image = d1.Image;
                doctor.Description = d1.Description;
                doctor.Fees = d1.Fees;
                doctor.Address = d1.Address;

                _context.Doctors.Update(doctor);
                _context.SaveChanges(); // Save changes to the database

                return true; // Successfully updated
            }
            catch (Exception ex)
            {
                // Handle exceptions, logging, etc.
                return false; // Update failed
            }
        }

        /*################################## Add Medicine list ##################################*/

        public void AddTreatmentPlan(int doctorId, int patientId, List<TreatmentPlanDTO> treatmentPlans)
        {
            try
            {
                var patient = _context.Patients.FirstOrDefault(p => p.Id == patientId);
                if (patient == null)
                {
                    throw new NullReferenceException("Patient not found!");
                }

                var doctor = _context.Doctors.FirstOrDefault(p => p.Id == doctorId);
                if (doctor == null)
                {
                    throw new NullReferenceException("Doctor not found!");
                }
                // Create a new treatment plan entity
                foreach (var treatmentPlan in treatmentPlans)
                {
                    var newTreatmentPlan = new MedicineAdvice
                    {
                        PatientId = patientId,
                        DoctorId = doctorId,
                        MedicineName = treatmentPlan.MedicineName,
                        Quantity = treatmentPlan.Quantity,
                        Frequency = treatmentPlan.Frequency
                    };

                    _context.MedicineAdvices.Add(newTreatmentPlan);
                }
                _context.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
        }

       
    }
}
