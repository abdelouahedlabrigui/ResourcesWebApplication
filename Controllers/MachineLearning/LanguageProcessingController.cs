using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResourcesWebApplication.Models.LanguageProcessing;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.MachineLearning;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace ResourcesWebApplication.Controllers.MachineLearning
{    
    [Route("[controller]")]
    public class LanguageProcessingController : Controller
    {
        public readonly ApplicationDbContext _context;
        public LanguageProcessingController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get200OKSatusTest()
        {
            return Ok();
        }

        [HttpPost]
        [Route("PostEntityRecognition")]
        public IActionResult PostEntityRecognition([FromBody] EntityRecognition entityRecognition)
        {
            try
            {
                string filename = @"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\data\Machine-Learning-for-History-Analysis\sample.txt";
                string datasetPath = @"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\library\Machine-Learning-for-History-Analysis\Columbus.py";
                
                // Check if the script has already been executed in the current session
                if (HttpContext.Session.GetString("ScriptExecuted") == null)
                {
                    // Mark the script as executed in the session
                    HttpContext.Session.SetString("ScriptExecuted", "true");

                    // Create the file if it doesn't exist
                    if (!System.IO.File.Exists(datasetPath + ".executed"))
                    {
                        System.IO.File.Create(datasetPath + ".executed").Dispose();
                    }

                    // Execute the script
                    using (Process process = new Process())
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = "cmd.exe";
                        startInfo.RedirectStandardInput = true;
                        startInfo.UseShellExecute = false;

                        process.StartInfo = startInfo;
                        process.Start();

                        StreamWriter sw = process.StandardInput;
                        if (sw.BaseStream.CanWrite)
                        {
                            sw.WriteLine(@"cd C:\Users\dell\Entrepreneurship\Engineering\machine_learning");
                            sw.WriteLine(@"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\ml\Scripts\activate");
                            sw.WriteLine(@"python.exe " + datasetPath  + @" """ + filename  + @"""");
                            sw.WriteLine(@"deactivate");
                            sw.Close();
                        }
                        process.WaitForExit();
                    }
                }
                return Ok(entityRecognition);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}