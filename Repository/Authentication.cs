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



        public ViewPatientDTO Login(LoginDTO login)
        {
            var c1 =
            context1.Patients.FirstOrDefault((p) => p.Email == login.Email && p.Password == login.Password);

            if (c1 != null) 
            { return new() {Id=c1.Id,Name=c1.Name,Image=c1.Image,Email=c1.Email,Password=c1.Password,Gender=c1.Gender,Address=c1.Address,Dob=c1.Dob,Phone=c1.Phone}; } 
            else { throw new NullReferenceException("Doctor not found or failed to update profile."); }

        }


        public bool RegisterDoctor(DoctorRegisterDTO doctotr)
        {
            try
            {
                if (context1.Doctors.Any(p => p.Email == doctotr.Email))
                {
                    return false;
                }

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

        public ViewDoctorDTO LoginDoctor(LoginDTO login)
        {
            var c1 =
            context1.Doctors.FirstOrDefault((p) => p.Email == login.Email && p.Password == login.Password);
            if (c1 != null) { return new() { Id = c1.Id, Name = c1.Name, Image = c1.Image, Email = c1.Email, Password = c1.Password, Gender = c1.Gender, Address = c1.Address, Fees = c1.Fees, Phone = c1.Phone ,Description=c1.Description, NoReviews =c1.NoReviews,Rating=c1.Rating, NoSessions =c1.NoSessions,AcceptanceStatus=c1.AcceptanceStatus}; } else { throw new NullReferenceException ("Patient not found or failed to update profile."); }

        }

    }
}
