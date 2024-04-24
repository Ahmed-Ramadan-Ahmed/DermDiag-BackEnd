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

namespace DermDiag.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly Authentication _context;
        private readonly DoctorRepository _doctorRepository;
        public DoctorsController(Authentication context, DoctorRepository doctorRepository)
        {
            _context = context;
            _doctorRepository = doctorRepository;
        }

        /*################################## REGISTERATION ##################################*/

        [HttpPost("RegisterDoctor")]

        public IActionResult RegisterDoctor(DoctorRegisterDTO register)
        {
            // Check uniqueness of email
            if (_context.RegisterDoctor(register)) { return Ok("Registration successful!"); } else { return Conflict("Email is already in use."); }

        }

        /*################################## LOGIN ##################################*/

        [HttpPost("LoginDoctor")]

        public IActionResult LoginDoctor(LoginDTO login)
        {

            if (_context.LoginDoctor(login)) { return Ok("Login Successfully"); } else { return Unauthorized("Unauthorized!!"); };
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
       
    }
}
