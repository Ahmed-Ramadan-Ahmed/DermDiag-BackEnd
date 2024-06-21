using DermDiag.DTO;
using DermDiag.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using RestSharp;
using System.Threading.Tasks;
using DermDiag.Migrations;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Azure;
using System.Numerics;
using Microsoft.IdentityModel.Tokens;

namespace DermDiag.Repository
{
    public class MeetingRepository
    {
        private readonly string API_KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJodHRwczovL2FjY291bnRzLmFwcGVhci5pbiIsImF1ZCI6Imh0dHBzOi8vYXBpLmFwcGVhci5pbi92MSIsImV4cCI6OTAwNzE5OTI1NDc0MDk5MSwiaWF0IjoxNzE4MTMzMDMzLCJvcmdhbml6YXRpb25JZCI6MjQwNjAwLCJqdGkiOiI3YmM1Y2ZhZC01YTc2LTQxNmItOGJkYi05OTBmZDczNzg3M2IifQ.uIy_mjn0lHv0UXjpGCVI1obRJcFT5BlrhN7BiRH5Nf0";
        private readonly DermDiagContext _context;
        public MeetingRepository(DermDiagContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateMeeting(BookDTO book)
        {
            using var client = new HttpClient();

            book.Date.AddHours(1);
            var data = new
            {
                endDate = book.Date,
                fields = new[] { "hostRoomUrl" }
            };

            var jsonData = JsonSerializer.Serialize(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", API_KEY);

            try
            {
                var response = await client.PostAsync("https://api.whereby.dev/v1/meetings", content);
                response.EnsureSuccessStatusCode();  // Throw an exception if the response status code is not successful

                var responseContent = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<JsonElement>(responseContent);
                var RoomUrl = responseData.GetProperty("roomUrl").GetString();
                var HostRoomURL = responseData.GetProperty("hostRoomUrl").GetString();

                MeetingDTO meetingDTO = new MeetingDTO
                {
                    DoctorLink = HostRoomURL,
                    PatientLink = RoomUrl,
                    Date = book.Date,
                    DoctorId = book.DoctorID,
                    PatientId = book.PatientID
                };

                var new_consultes = new Consulte
                {
                    PatientLink = RoomUrl,
                    DoctorLink = HostRoomURL,
                    Date = book.Date,
                    DoctorId = book.DoctorID,
                    PatientId = book.PatientID
                };
                _context.Consultes.Add(new_consultes);
                _context.SaveChanges();

                return true;
            }
            catch (HttpRequestException e)
            {
                throw new Exception(e.Message);

                MeetingDTO meetingDTO = new MeetingDTO
                {
                    DoctorLink = "error",
                    PatientLink = $"Request error: {e.Message}"
                };

                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                MeetingDTO meetingDTO = new MeetingDTO
                {
                    DoctorLink = "error",
                    PatientLink = $"Unexpected error: {e.Message}"
                };
                return false;
            }
        }

        public List<MeetingDTO> GetMeetingDTO(int id, bool IsDoctor)
        {
            List<MeetingDTO> meetings;
            if (IsDoctor)
            {
                meetings = _context.Consultes
                    .Where(consulte => consulte.DoctorId == id && consulte.Date > DateTime.Now)
                    .Select(consulte => new MeetingDTO
                    {
                        PatientId = consulte.PatientId,
                        DoctorId = consulte.DoctorId,
                        Date = consulte.Date,
                        PatientLink = consulte.PatientLink,
                        DoctorLink = consulte.DoctorLink    
                    })
                    .ToList();
            }
            else
            {
                meetings = _context.Consultes
                    .Where(consulte => consulte.PatientId == id && consulte.Date > DateTime.Now)
                    .Select(consulte => new MeetingDTO
                    {
                        PatientId = consulte.PatientId,
                        DoctorId = consulte.DoctorId,
                        Date = consulte.Date,
                        PatientLink = consulte.PatientLink,
                        DoctorLink = consulte.DoctorLink
                    })
                    .ToList();
           
            }

            if(meetings.IsNullOrEmpty())
            {
                throw new Exception("No Meetings Found");
            }

            return (meetings);
        }
    }
}