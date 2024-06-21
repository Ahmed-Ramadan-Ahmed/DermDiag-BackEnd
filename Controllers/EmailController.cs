using DermDiag.DTO;
using DermDiag.Models;
using DermDiag.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System;

namespace DermDiag.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly EmailRepository _emailRepository;

        public EmailController(EmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] EmailMessage emailMessage)
        {
            /*
            emailMessage.Body = @"
                Dear doaa,<br><br>
                We at DermDiag want to take a moment to wish you and your loved ones a joyous and blessed celebration.<br><br>
                May this Eid be filled with happiness, good health, and delicious food.
                Enjoy the festivities, delicious meals, and the time spent with your family and friends. We hope you have a safe and memorable Eid.<br><br>
                We appreciate your dedication and hard work throughout the year. Your contributions are invaluable to the DermDiag system, and we are grateful to have you as part of our DermDiag family.<br><br>
                Eid Mubarak!<br><br>
                Best regards,<br>
                The DermDiag Team
                "; 
            */
            if (emailMessage == null || string.IsNullOrEmpty(emailMessage.To) || string.IsNullOrEmpty(emailMessage.Subject) || string.IsNullOrEmpty(emailMessage.Body))
            {
                return BadRequest("Email message is incomplete.");
            }
            await _emailRepository.SendEmailAsync(emailMessage);
            return Ok("Email sent successfully.");
        }
    }
}
