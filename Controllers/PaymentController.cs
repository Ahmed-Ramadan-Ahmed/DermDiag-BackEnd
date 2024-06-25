using DermDiag.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using DermDiag.Models;
using DermDiag.Repository;
using Microsoft.Extensions.Options;



namespace DermDiag.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentRepository _paymentRepository;
        private readonly DermDiagContext _context;
        private readonly EmailRepository _emailRepository;
        private readonly EmailTemplates _emailTemplates;
        public PaymentsController(DermDiagContext context, PaymentRepository paymentRepository, EmailRepository emailRepository, IOptions<EmailTemplates> emailTemplates)
        {
            _paymentRepository = paymentRepository;
            _context = context;
            _emailRepository = emailRepository;
            _emailTemplates = emailTemplates.Value;
        }
        /*
        [HttpPost("CreatPayment")]
        public async Task<IActionResult> CreatPayment(PaymentDTO paymentDTO)
        {
            try
            {
                return Ok("https://buy.stripe.com/test_5kA4jL02Q4mL95C8ww");

                var token = await _paymentRepository.AuthenticateAsync();
                var amount = _context.Doctors.FirstOrDefault(d => d.Id == paymentDTO.ReceiverID).Fees;
                var id = await _paymentRepository.CreateOrderAsync(token,amount);
                var check = await _paymentRepository.GetPaymentStatusAsync(id);
                //return Ok(check);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
*/

        [HttpPost("Send Payment Invoice Mail")]
        public async Task<IActionResult> SendPaymentInvoiceMail(PaymentDTO paymentDTO)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.Id == paymentDTO.SenderID);
            var doctor = _context.Doctors.FirstOrDefault(d => d.Id == paymentDTO.ReceiverID);

            EmailMessage emailMessage1 = new EmailMessage()
            {
                To = patient.Email,
                Subject = "💸🧾Transaction Done with New invoice from DermDiag!",
                Body = _emailTemplates.Invoice
                                          .Replace("June 24, 2024", DateTime.Now.ToString())
                                          .Replace("ahmed",patient.Name)
                                          .Replace("Customer Name","Dr. " + doctor.Name)
                                          .Replace("Service/Product 1","DermDiag Online Consult")
            };
            await _emailRepository.SendEmailAsync(emailMessage1);

            emailMessage1.To = doctor.Email;
            await _emailRepository.SendEmailAsync(emailMessage1);



            return Ok(true);

        }
    }

}
