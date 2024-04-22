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

        // GET: api/Patients
        [HttpGet("GetAllDoctors")]
        public ActionResult<IEnumerable<DoctorHomeDTO>> GetAll(int id)
        {
            try
            {
                return Ok(_patientRepository.GetAll(id));
            }catch (Exception ex)
            {
                return NotFound();  
            }
            
        }

        //// GET: api/Patients/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Patient>> GetPatients(int id)
        //{
        //    var patients = await _context.Patients.FindAsync(id);

        //    if (patients == null)
        //    {
        //        return NotFound();
        //    }

        //    return patients;
        //}

        //// PUT: api/Patients/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutPatients(int id, Patient patients)
        //{
        //    if (id != patients.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(patients).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PatientsExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Patients
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Register")]

        public IActionResult Register(RegisterrDTO register)
        {
            // Check uniqueness of email
            if (_context.Register(register)) { return Ok("Registration successful!"); } else { return Conflict("Email is already in use."); }
           
        }

        [HttpPost("Login")]

        public IActionResult Login(LoginDTO login)
        {

            if (_context.Login(login)) { return Ok(); } else { return Unauthorized(); };
        }





        // DELETE: api/Patients/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePatients(int id)
        //{
        //    var patients = await _context.Patients.FindAsync(id);
        //    if (patients == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Patients.Remove(patients);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool PatientsExists(int id)
        //{
        //    return _context.Patients.Any(e => e.Id == id);
        //}
    }
}
