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
            var doctor = _context.Doctors.Include(d => d.Patients).FirstOrDefault(d => d.Id == Id);
            if (doctor == null)
            {
                throw new Exception("Doctor not found!");
            }

            if (doctor.Patients == null || doctor.Patients.Count == 0)
            {
                throw new Exception("No patients associated with this doctor!");
            }

            List<PatientHomeDTO> returnPatients = new List<PatientHomeDTO>();
            foreach (var patient in doctor.Patients)
            {
                var appointmentDate = _context.Books.FirstOrDefault(b => b.DoctorId == Id && b.PatientId == patient.Id)?.AppointmentDate;
                returnPatients.Add(new PatientHomeDTO
                {
                    Name = patient.Name,
                    Image = patient.Image,
                    AppointmentDate = appointmentDate
                });
            }
            return returnPatients;
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
