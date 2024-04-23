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

        //public List<PatientHomeDTO> SearchPatientsByName(string patientName)
        //{
        //    var patients = _context.Patients
        //        .Where(d => EF.Functions.Like(d.Name, $"%{patientName}%"));

        //    List<PatientHomeDTO> ReturnPatients = new List<PatientHomeDTO>();
        //    foreach (var doctor in patients)
        //    {
        //        ReturnPatients.Add(new PatientHomeDTO()
        //        {
        //            Name = doctor.Name,
        //            Image = doctor.Image,

        //        });
        //    }
        //    return ReturnPatients;
        //}

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
