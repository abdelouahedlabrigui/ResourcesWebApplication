using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResourcesWebApplication.Models.Context;
using Renci.SshNet;


namespace ResourcesWebApplication.Controllers.Certificates
{
    [Route("[controller]")]
    public class CertificatesController : Controller
    {
        public readonly ApplicationDbContext _context;
        public CertificatesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("StatusCODE200")]
        public IActionResult StatusCODE200()
        {
            return Ok();
        }
        [HttpGet]
        [Route("GenerateGetRSAKey")]
        public IActionResult GenerateGetRSAKey(string output_file, string rsa_size)
        {
            try
            {
                string ipADDRESS = "192.168.137.129";
                string username = "abdelouahedlabrigui";
                string password = "rootroot";
                using (var client = new SshClient(ipADDRESS, username, password))
                {
                    client.Connect();
                    if (client.IsConnected)
                    {
                        string command = $"openssl genrsa -out {output_file} {rsa_size}"; // The command to execute on the remote server
                        var output = client.RunCommand(command);
                        client.Disconnect();                        
                        
                        return Ok(output.Result);
                    }
                    else
                    {
                        return Content("Failed to connect to the remote server.");
                    }
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}