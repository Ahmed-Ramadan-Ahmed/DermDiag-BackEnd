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

        //public List<DoctorHomeDTO> SearchDoctorsByName(string doctorName, int PId)
        //{
        //    var docotrs = context1.Doctors
        //        .Where(d => EF.Functions.Like(d.Name, $"%{doctorName}%"));
        //    var patient = context1.Patients.Include(p => p.Doctors).FirstOrDefault(p => p.Id == PId);

        //    if (patient == null)
        //    {
        //        throw new Exception("Not Found!");
        //    }

        //    List <DoctorHomeDTO> ReturnDoctors = new List<DoctorHomeDTO>();
        //    foreach (var doctor in docotrs)
        //    {
        //        ReturnDoctors.Add(new DoctorHomeDTO()
        //        {
        //            Image = doctor.Image,
        //            Id = doctor.Id,
        //            Name = doctor.Name,
        //            Rating = doctor.Rating,
        //            IsFavourite = patient.Doctors.FirstOrDefault(d => d.Id == doctor.Id) != null ? true : false,
        //        });
        //    }
        //    return ReturnDoctors;
        //}


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
    }
}
