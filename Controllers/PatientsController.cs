using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DermDiag.DTO;
using DermDiag.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DermDiag.Models;
using Microsoft.PowerBI.Security;
using Microsoft.Extensions.Options;
using System.Numerics;

namespace DermDiag.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly Authentication _context;
        private readonly PatientRepository _patientRepository;
        private readonly EmailRepository _emailRepository;
        private readonly EmailTemplates _emailTemplates;
        private readonly DermDiagContext _database;

        public PatientsController(Authentication context, PatientRepository patientcontext, IOptions<EmailTemplates> emailTemplates, EmailRepository emailRepository, DermDiagContext database)
        {
            _context = context;
            _patientRepository = patientcontext;
            _emailTemplates = emailTemplates.Value;
            _emailRepository = emailRepository;
            _database = database;
        }


        //// PUT: api/Patients/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754


        //// POST: api/Patients
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754


        /*################################## REGISTERATION ##################################*/

        [HttpPost("Register")]

        public async Task<IActionResult> Register(RegisterrDTO register)
        {
            // Check uniqueness of email
            if (_context.Register(register)) 
            {
                EmailMessage emailMessage = new EmailMessage()
                {
                    To = register.Email,
                    Subject = "Welcome to DermDiag✨ Unlock Your Skin Health Journey with Your New Account",
                    Body = _emailTemplates.WelcomeEmail
                                          .Replace("Doaa_Nady", register.Name)
                                          .Replace("12345678", register.Password)
                                          .Replace("Dear Doaa", "Dear " + register.Name)

                };
                await _emailRepository.SendEmailAsync(emailMessage);

                return Ok("Registration successful!"); 
            } 
            else 
            {
                return Conflict("Email is already in use."); 
            }

        }

        /*################################## LOGIN ##################################*/


        [HttpPost("Login")]

        public async Task<IActionResult> Login(LoginDTO login)
        {

            try
            {
                var LoginStatus = _context.Login(login);
                if(LoginStatus != null) 
                {
                    var patient = _database.Patients.FirstOrDefault(d => d.Email == login.Email);

                    EmailMessage emailMessage = new EmailMessage()
                    {
                        To = login.Email,
                        Subject = "⚠️ Security Alert: Recent Login Attempt to Your DermDiag Account",
                        Body = _emailTemplates.LoginAttemptNotification
                                          .Replace("Dear Doaa", "Dear " + patient.Name)
                                          .Replace("22 Jun 2024, 23:25 PM", DateTime.Now.ToString())
                    };
                    await _emailRepository.SendEmailAsync(emailMessage);
                }

                return Ok(LoginStatus);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        /*################################## GET ALL DOCTORS ##################################*/

        [HttpGet("GetAllDoctors")]
        public ActionResult<IEnumerable<DoctorHomeDTO>> GetAllDoctors(int id)
        {
            try
            {
                return Ok(_patientRepository.GetAllDoctors(id));
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }


        /*################################## SEARCH FOR DOCTORS ##################################*/

        [HttpGet("SearchDoctors")]
        public ActionResult<IEnumerable<DoctorHomeDTO>> SearchDoctorsByName(string DoctorName, int P_Id)
        {
            try
            {
                return Ok(_patientRepository.SearchDoctorsByName(DoctorName, P_Id));
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }

       
        /*################################## ADD FAVORITE DOCTORS ##################################*/

        [HttpPost("AddFavoriteDoctors")]

        public IActionResult AddFavoriteDoctors(int patientId, int doctorId)
        {
            try
            {
                if (_patientRepository.AddFavoriteDoctors(patientId, doctorId))
                {
                    return Ok("Favorite doctor added successfully!");
                }
                else
                {
                    return NotFound("Patient or doctor not found, or already added as favorite.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request."); // Internal Server Error
            }
        }


        /*################################## GET FAVORITE DOCTORS ##################################*/

        [HttpGet("GetFavoriteDoctors")]
        public ActionResult<IEnumerable<DoctorHomeDTO>> FavoriteDoctors(int id)
        {
            try
            {
                return Ok(_patientRepository.FavoriteDoctors(id));
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }


        /*################################## REMOVE FAVORITE DOCTORS ##################################*/

        [HttpDelete("RemoveFavoriteDoctors")]
        public IActionResult RemoveFavoriteDoctors(int patientId, int doctorId)
        {
            try
            {
                if (_patientRepository.RemoveFavoriteDoctors(patientId, doctorId))
                {
                    return Ok("Favorite doctor removed successfully!");
                }
                else
                {
                    return NotFound("Patient or doctor not found, or not in favorites list.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        /*################################## Update Profile ##################################*/

        [HttpPut("EditProfilePatient")]
        public IActionResult UpdatePatientProfile(UpdatePatientDTO Ubdate, int patientId)
        {

            try
            {
                return Ok(_patientRepository.UpdatePatientProfile(Ubdate, patientId));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        
        /*################################## TreatmentPlan ##################################*/


        [HttpGet("GetTreatmentPlan")]
        public IActionResult GetTreatmentPlan(int doctorId, int patientId)
        {

            try
            {

                return Ok(_patientRepository.GetTreatmentPlan(doctorId, patientId));
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }

        }

        /*################################## Add Task ##################################*/

        [HttpPost("AddTask")]
        public IActionResult AddTask(int patientId, TaskDTO task)
        {
            try
            {
                _patientRepository.AddTask(patientId, task);
                return Ok("Task added successfully!");
            }
            
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        /*################################## Remove Task ##################################*/

        [HttpPost("RemoveTask")]
        public IActionResult DeleteTasks(int P_ID, int task_Id)
        {
            try
            {
                _patientRepository.DeleteTask(P_ID, task_Id);
                return Ok("Task Removed successfully!");

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        /*################################## GET Tasks ##################################*/


        [HttpGet("GetTasks")]
        public IActionResult GetTasks(int patientId)
        {
            try
            { 
                return Ok(_patientRepository.GetTasks(patientId));
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }

        }

        /*################################## Add Review ##################################*/
        [HttpPost("AddReview")]
        public IActionResult AddReview(int doctorId, int patientId,ReviewDto review)
        {
            try
            {
                _patientRepository.AddReview(doctorId,patientId,review);
                return Ok("Review added successfully!");
            }

            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
        /*################################## Update Review ##################################*/

        [HttpPut("UpdateReview")]
        public IActionResult UpdateReview(int doctorId, int patientId, ReviewDto review)
        {
            try
            {
                return Ok(_patientRepository.UpdateReview(doctorId, patientId, review));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        /*################################## Delete Review ##################################*/

        [HttpDelete("DeleteReview")]
        public IActionResult DeleteReview(int doctorId, int patientId)
        {
            try
            {
                if (_patientRepository.DeleteReview(doctorId,patientId))
                {
                    return Ok("Review removed successfully!");
                }
                else
                {
                    return NotFound("Patient or doctor not found, or not in Review list.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
        /*################################## Get All Reviews ##################################*/

        [HttpGet("AllReviews")]
        public ActionResult<IEnumerable<GetReviewDTO>> GetAllReviews(int doctorId)
        {
            try
            {
                return Ok(_patientRepository.GetAllReviews(doctorId));
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }

        /*################################## Book ##################################*/
        [HttpPost("CreateBook")]
        public IActionResult CreateBook(BookDTO book)
        {
            try
            {
                return Ok(_patientRepository.CreateBook(book));
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AccessViolationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}

    
