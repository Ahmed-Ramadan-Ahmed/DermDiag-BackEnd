using DermDiag.Repository;
using Microsoft.AspNetCore.Mvc;
using DermDiag.Models;
using DermDiag.DTO;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Policy;

namespace DermDiag.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly Authentication _context;
        private readonly MeetingRepository _meetingRepository;

        public MeetingController(Authentication context, MeetingRepository meetingRepository)
        {
            _context = context;
            _meetingRepository = meetingRepository;
        }

        [HttpPost("CreateMeeting")]
        public async Task<bool> CreateMeeting(BookDTO book)
        {
            var response = await _meetingRepository.CreateMeeting(book);
            return response;
        }

        [HttpGet("Get Meeting Information")]
        public ActionResult<List<MeetingDTO>> GetMeetingInfo(int Id, bool IsDoctor)
        {
            try
            {
                return Ok(_meetingRepository.GetMeetingDTO(Id, IsDoctor));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
