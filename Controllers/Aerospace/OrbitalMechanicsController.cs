using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ResourcesWebApplication.Controllers.Aerospace
{
    [Route("[controller]")]
    public class OrbitalMechanicsController : Controller
    {
        public IActionResult KinematicEquations()
        {
            return View();
        }
    }
}