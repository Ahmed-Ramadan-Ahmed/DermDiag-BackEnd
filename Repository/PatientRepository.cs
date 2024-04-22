using DermDiag.DTO;
using DermDiag.Models;
using Microsoft.EntityFrameworkCore;

namespace DermDiag.Repository
{
    public class PatientRepository
    {
        private readonly DermDiagContext context1;
        public PatientRepository(DermDiagContext context)
        {
            context1 = context;
        }

        public List<DoctorHomeDTO> GetAll(int Id) 
        {
            var docotrs = context1.Doctors.ToList(); 
            var patient = context1.Patients.Include(p => p.Doctors).FirstOrDefault(p => p.Id == Id );

            if ( patient == null )
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
                }) ;
            }
            return ReturnDoctors;
        }
    }
}
