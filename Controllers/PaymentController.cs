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

namespace DermDiag.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentRepository _paymentRepository;
        private readonly DermDiagContext _context;
        public PaymentsController(DermDiagContext context, PaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
            _context = context;
        }

        [HttpPost("CreatPayment")]
        public async Task<IActionResult> CreatPayment(PaymentDTO paymentDTO)
        {

            try
            {

                return Ok("https://buy.stripe.com/test_5kA4jL02Q4mL95C8ww");

                var token = await _paymentRepository.AuthenticateAsync();

                //return Ok("https://accept.paymob.com/api/acceptance/iframes/852430?payment_token=" + token) ;


                var amount = _context.Doctors.FirstOrDefault(d => d.Id == paymentDTO.ReceiverID).Fees;
                amount = 100;
                var url = await _paymentRepository.CreateOrderAsync(token,amount);

                return Ok(url);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }

}
