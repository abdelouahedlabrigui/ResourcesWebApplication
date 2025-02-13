using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ResourcesWebApplication.Controllers.Aerospace
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrbitalMechanicsAPIController : ControllerBase
    {
        [HttpGet]
        [Route("AngleResults")]
        public IActionResult AngleResults()
        {
            // Define altitude (h) and speed (v) of the airplane
            double altitude = 10000; // example altitude in meters
            double speed = 200; // example speed in m/s
            
            // Calculate values for angles from 0 to 90 degrees
            var data = Enumerable.Range(0, 90).Select(thetaDeg =>
            {
                double theta = Math.PI * thetaDeg / 180.0; // Convert to radians
                
                // Angular velocity θ˙
                double angularVelocity = (speed * Math.Cos(theta)) / altitude;
                
                // Angular acceleration θ¨
                double angularAcceleration = -(speed * speed * Math.Sin(theta)) / (altitude * altitude);
                
                // Radial velocity r˙
                double radialVelocity = speed * Math.Sin(theta);
                
                return new
                {
                    Theta = thetaDeg,
                    AngularVelocity = angularVelocity,
                    AngularAcceleration = angularAcceleration,
                    RadialVelocity = radialVelocity
                };
            }).ToList();
            return Ok(data);
        }
        [HttpGet]
        [Route("CalculateData")]
        public IActionResult CalculateData(double altitude, double speed)
        {
            // Parameters
            double h = altitude;
            double v = speed;

            // Calculate data for a range of angles from 0 to 90 degrees
            var data = Enumerable.Range(0, 90).Select(thetaDeg =>
            {
                double theta = Math.PI * thetaDeg / 180.0; // Convert to radians
                
                // Angular velocity θ˙
                double angularVelocity = (v * Math.Cos(theta)) / h;
                
                // Angular acceleration θ¨
                double angularAcceleration = -(v * v * Math.Sin(theta)) / (h * h);
                
                // Radial velocity r˙
                double radialVelocity = v * Math.Sin(theta);
                
                return new
                {
                    Theta = thetaDeg,
                    AngularVelocity = angularVelocity,
                    AngularAcceleration = angularAcceleration,
                    RadialVelocity = radialVelocity
                };
            }).ToList();

            // Return the data with Ok status
            return Ok(data);
        }
    }
}