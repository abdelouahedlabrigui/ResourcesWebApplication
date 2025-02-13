using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Documents;
using ResourcesWebApplication.Models.Responses;

namespace ResourcesWebApplication.Controllers.Scraping
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataTransactionApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DataTransactionApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-documents")]
        public async Task<IActionResult> GetDocuments()
        {
            try
            {
                var documents = await _context.Documents?.ToListAsync();
                if (documents.Count == 0)
                {
                    return NoContent();
                }
                HttpClient httpClient = new HttpClient();

                List<RequestResponse> requestResponses = new List<RequestResponse>();
                foreach (var document in documents)
                {
                    string title = document.Title;
                    string url = document.Url;  
                    DateTime createdAT = document.CreatedAT;   
                    RequestResponse response = 
                        await httpClient.GetFromJsonAsync<RequestResponse>($"http://localhost:5261/api/Documents?title={title}&url={url}&createdAT={createdAT}");

                    
                    requestResponses.Add(response);
                }
                return Ok(new RequestResponse{ Message = $"Total Documents: {documents.Count} | Inserted Documents: {requestResponses.Count}" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}