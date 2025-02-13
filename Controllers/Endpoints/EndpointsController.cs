using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Web;
using ResourcesWebApplication.Models.Endpoints;
using Microsoft.AspNetCore.Mvc.Routing;
using ResourcesWebApplication.Middlewares.Interfaces;

namespace ResourcesWebApplication.Controllers.Endpoints
{
    [Route("[controller]")]
    public class EndpointsController : Controller
    {
        private readonly IRouteProvider _routeProvider;
        public EndpointsController(IRouteProvider routeProvider)
        {
            _routeProvider = routeProvider;
        }
        public IActionResult GetByMiddlewareEndPoints()
        {
            var routes = _routeProvider.GetRoutes();
            return Ok(routes);
        }
    }
}