using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Exceptions;
using ResourcesWebApplication.Models.LanguageProcessing;
using ResourcesWebApplication.Models.MachineLearning.Datasets;


namespace ResourcesWebApplication.Controllers.Nlp
{
    [Route("[controller]")]
    public class LinguisticFeaturesController : Controller
    {
        public readonly ApplicationDbContext _context;
        public LinguisticFeaturesController(ApplicationDbContext context)
        {
            _context = context;
        }
        string controllerName = "LinguisticFeaturesController";
        string loggedAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        [HttpGet]
        public IActionResult Get200OKSatusTest()
        {
            return Ok();
        }
    }
}