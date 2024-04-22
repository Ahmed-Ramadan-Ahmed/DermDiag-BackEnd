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

        public DoctorsController(Authentication context)
        {
            _context = context;
        }


        // GET: api/Doctors
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        //{
        //    return await _context.Doctors.ToListAsync();
        //}

        //// GET: api/Doctors/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Doctor>> GetDoctor(int id)
        //{
        //    var doctor = await _context.Doctors.FindAsync(id);

        //    if (doctor == null)
        //    {
        //        return NotFound();
        //    }

        //    return doctor;
        //}


        [HttpPost("RegisterDoctor")]

        public IActionResult RegisterDoctor(DoctorRegisterDTO register)
        {
            // Check uniqueness of email
            if (_context.RegisterDoctor(register)) { return Ok("Registration successful!"); } else { return Conflict("Email is already in use."); }

        }

        [HttpPost("LoginDoctor")]

        public IActionResult LoginDoctor(LoginDTO login)
        {

            if (_context.LoginDoctor(login)) { return Ok(); } else { return Unauthorized(); };
        }

        // PUT: api/Doctors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDoctor(int id, Doctor doctor)
        //{
        //    if (id != doctor.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(doctor).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DoctorExists(id))
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

        //// POST: api/Doctors
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Doctor>> PostDoctor(Doctor doctor)
        //{
        //    _context.Doctors.Add(doctor);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (DoctorExists(doctor.Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetDoctor", new { id = doctor.Id }, doctor);
        //}

        //// DELETE: api/Doctors/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteDoctor(int id)
        //{
        //    var doctor = await _context.Doctors.FindAsync(id);
        //    if (doctor == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Doctors.Remove(doctor);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool DoctorExists(int id)
        //{
        //    return _context.Doctors.Any(e => e.Id == id);
        //}
    }
}
