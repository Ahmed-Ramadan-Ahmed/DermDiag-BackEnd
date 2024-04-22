using DermDiag.DTO;
using DermDiag.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace DermDiag.Repository
{
    public class Authentication
    {
        private readonly DermDiagContext context1;
        public Authentication(DermDiagContext context)
        {
            context1 = context;
        }


        public bool Register(RegisterrDTO patient)
        {
            try
            {
                if (context1.Patients.Any(p => p.Email == patient.Email))
                {
                    return false;
                }

                // Create a new Patient object
                var newPatient = new Patient
                {
                    Name = patient.Name,
                
                    Email = patient.Email,
                    Password = patient.Password,
                    Phone = patient.Phone,
                    Gender = patient.Gender,
                    Dob = patient.Dob,
                };

                context1.Patients.Add(newPatient);
                context1.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public bool Login(LoginDTO login)
        {
            var c1 =
            context1.Patients.FirstOrDefault((p) => p.Email == login.Email && p.Password == login.Password);
            if (c1 != null) { return true; } else { return false; }

        }


        public bool RegisterDoctor(DoctorRegisterDTO doctotr)
        {
            try
            {
                if (context1.Doctors.Any(p => p.Email == doctotr.Email))
                {
                    return false;
                }

                // Create a new Patient object
                var newDoctor = new Doctor
                {
                    Name = doctotr.Name,

                    Email = doctotr.Email,
                    Password = doctotr.Password,
                    Phone = doctotr.Phone,
                    Gender = doctotr.Gender,
                    Address = doctotr.Address,
                };

                context1.Doctors.Add(newDoctor);
                context1.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool LoginDoctor(LoginDTO login)
        {
            var c1 =
            context1.Doctors.FirstOrDefault((p) => p.Email == login.Email && p.Password == login.Password);
            if (c1 != null) { return true; } else { return false; }

        }

    }
}
