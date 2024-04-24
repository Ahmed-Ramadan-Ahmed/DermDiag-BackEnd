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
    }
}
