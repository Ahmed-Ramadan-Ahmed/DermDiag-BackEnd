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

namespace DermDiag.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly Authentication _context;
        private readonly PatientRepository _patientRepository;

        public PatientsController(Authentication context, PatientRepository patientcontext)
        {
            _context = context;
            _patientRepository = patientcontext;
        }


        //// PUT: api/Patients/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754


        //// POST: api/Patients
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754


        /*################################## REGISTERATION ##################################*/

        [HttpPost("Register")]

        public IActionResult Register(RegisterrDTO register)
        {
            // Check uniqueness of email
            if (_context.Register(register)) { return Ok("Registration successful!"); } else { return Conflict("Email is already in use."); }

        }

        /*################################## LOGIN ##################################*/


        [HttpPost("Login")]

        public IActionResult Login(LoginDTO login)
        {

            if (_context.Login(login)) { return Ok(); } else { return Unauthorized(); };
        }


        /*################################## LOGOUT ##################################*/



        /*################################## GET ALL DOCTORS ##################################*/

        [HttpGet("GetAllDoctors")]
        public ActionResult<IEnumerable<DoctorHomeDTO>> GetAll(int id)
        {
            try
            {
                return Ok(_patientRepository.GetAll(id));
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

        [HttpPost("RemoveFavoriteDoctors")]
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


    }
}
