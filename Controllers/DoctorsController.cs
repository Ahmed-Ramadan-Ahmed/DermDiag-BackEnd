using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DermDiag.Models;
using DermDiag.DTO;
using DermDiag.Repository;
using System.IO;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using DermDiag.Models;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Org.BouncyCastle.Utilities;
using MimeKit.Utils;


namespace DermDiag.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly Authentication _context;
        private readonly DoctorRepository _doctorRepository;
        private readonly EmailRepository _emailRepository;
        private readonly EmailTemplates _emailTemplates;
        private readonly DermDiagContext _database;
        public DoctorsController(Authentication context, DoctorRepository doctorRepository, IOptions<EmailTemplates> emailTemplates, EmailRepository emailRepository , DermDiagContext database)
        {
            _context = context;
            _doctorRepository = doctorRepository;
            _emailTemplates = emailTemplates.Value;
            _emailRepository = emailRepository;
            _database = database;
        }

        /*################################## REGISTERATION ##################################*/

        [HttpPost("RegisterDoctor")]

        public async Task<IActionResult> RegisterDoctor(DoctorRegisterDTO register)
        {
            if (_context.RegisterDoctor(register)) 
            {

                EmailMessage emailMessage = new EmailMessage()
                {
                    To = register.Email,
                    Subject = "Welcome to DermDiag✨ Unlock Your Skin Health Journey with Your New Account",
                    Body = _emailTemplates.WelcomeEmail
                                          .Replace("Doaa_Nady", register.Name)
                                          .Replace("12345678", register.Password)
                                          .Replace("Dear Doaa","Dear Dr. "+register.Name)

                };
                await _emailRepository.SendEmailAsync(emailMessage);



                return Ok("Registration successful!"); 
            }
            else 
            {
                return Conflict("Not valid Email or already in use."); 
            }

        }

        /*################################## LOGIN ##################################*/

        [HttpPost("LoginDoctor")]

        public async Task<IActionResult> LoginDoctor(LoginDTO login)
        {
            try
            {
                var LoginStatus = _context.LoginDoctor(login);
                var doctor = _database.Doctors.FirstOrDefault(d => d.Email == login.Email);

                if (doctor == null)
                {
                    return Ok(LoginStatus);
                }

                EmailMessage emailMessage = new EmailMessage()
                {
                    To = login.Email,
                    Subject = "⚠️ Security Alert: Recent Login Attempt to Your DermDiag Account",
                    Body = _emailTemplates.LoginAttemptNotification
                                          .Replace("Dear Doaa", "Dear Dr. " + doctor.Name)
                                          .Replace("22 Jun 2024, 23:25 PM" , DateTime.Now.ToString() )
                };
                await _emailRepository.SendEmailAsync(emailMessage);


                return Ok(LoginStatus);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /*################################## SEARCH FOR PATIENTS ##################################*/


        [HttpGet("SearchPatients")]
        public ActionResult<IEnumerable<PatientHomeDTO>> SearchPatientsByName(string patientName)
        {
            try
            {
                return Ok(_doctorRepository.SearchPatientsByName(patientName));
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }


        /*################################## GET ALL PATIENTS ##################################*/

        [HttpGet("GetAllPatients")]
        public ActionResult<IEnumerable<PatientHomeDTO>> GetAllPatients(int id)
        {
            try
            {
                return Ok(_doctorRepository.GetAllPatients(id));
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }

        // PUT: api/Doctors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        //// POST: api/Doctors
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        ///
        /*################################## Update Profile ##################################*/

        [HttpPut("EditProfileDoctor")]
        public IActionResult UpdateDoctorProfile(UpdateDoctorDtO Update,int doctorId)
        {

            try
            {
                return Ok(_doctorRepository.UpdateDoctorProfile(Update, doctorId));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        /*################################## Add Medicines ##################################*/


        [HttpPost("AddMedicines")]
        public IActionResult AddMedicines(int doctorId, int patientId, List<TreatmentPlanDTO> treatments) {

            try
            {
                _doctorRepository.AddTreatmentPlan(doctorId, patientId, treatments);
                return Ok(); 
            }
            catch (NullReferenceException ex){
                return NotFound(ex.Message); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request."); 
            }

        }
      
    }

}
