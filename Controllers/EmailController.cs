using DermDiag.DTO;
using DermDiag.Models;
using DermDiag.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System;
using System.Numerics;
using Microsoft.Extensions.Options;

namespace DermDiag.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly EmailRepository _emailRepository;
        private readonly DermDiagContext _context;
        private readonly EmailTemplates _emailTemplates;
        public EmailController(EmailRepository emailRepository, DermDiagContext context, IOptions<EmailTemplates> emailTemplates)
        {
            _emailRepository = emailRepository;
            _context = context;
            _emailTemplates = emailTemplates.Value;
        }

        [HttpPost("Get Verification Code")]
        public async Task<string> VerifyAcount(VerificationDTO verificationDTO)
        {

            if (verificationDTO.Email == null || 
                _context.Doctors.FirstOrDefault(d => d.Email == verificationDTO.Email) != null ||
                _context.Patients.FirstOrDefault(p => p.Email == verificationDTO.Email) != null) 
            {
                return "Not valid Email or is already in use.";
            }
            
            Random random = new Random();
            int minValue = 100000,  maxValue = 999999;
            int randomNumber = random.Next(minValue, maxValue + 1);

            var code = randomNumber.ToString();

            EmailMessage emailMessage = new EmailMessage()
            {
                To = verificationDTO.Email,
                Subject = "DermDiag Acount Verification 👀",
                Body = _emailTemplates.Verify
                                          .Replace("User Name", verificationDTO.Name)
                                          .Replace("Verification Code",code)
            };
            await _emailRepository.SendEmailAsync(emailMessage);


            return code;
        }

        /*
        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] EmailMessage emailMessage)
        {
            if (emailMessage == null || string.IsNullOrEmpty(emailMessage.To) || string.IsNullOrEmpty(emailMessage.Subject) || string.IsNullOrEmpty(emailMessage.Body))
            {
                return BadRequest("Email message is incomplete.");
            }
            await _emailRepository.SendEmailAsync(emailMessage);
            return Ok("Email sent successfully.");
        }
        */
    }
}
