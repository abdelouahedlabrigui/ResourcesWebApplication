using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResourcesWebApplication.Models.Endpoints;

namespace ResourcesWebApplication.Middlewares.Interfaces
{
    public interface IRouteProvider
    {
        IEnumerable<UrlMetadata> GetRoutes();
    }
}