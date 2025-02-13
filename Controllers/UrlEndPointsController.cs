using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Endpoints;
using ResourcesWebApplication.Middlewares.Interfaces;
using ResourcesWebApplication.Models.MachineLearning.Datasets;
using Microsoft.AspNetCore.Routing;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.ML.Transforms;
using System.IO;
using System.Text;
using System.Data;
using Microsoft.Data.SqlClient;
using ResourcesWebApplication.Models.Requests;



namespace ResourcesWebApplication.Controllers
{
    public class UrlEndPointsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRouteProvider _routeProvider;
        private readonly HttpClient _httpClient;
        public UrlEndPointsController(ApplicationDbContext context, IRouteProvider routeProvider)
        {
            _context = context;
            _routeProvider = routeProvider;
            _httpClient = new HttpClient();
        }
        public IActionResult Download()
        {
            return View();
        }
        public IActionResult WebPage()
        {
            return View();
        }
        // RequestSuccesses
        public async Task<IActionResult> GetRequestSuccess()
        {
            try
            {
                var data = await _context.RequestSuccesses.Take(1).OrderByDescending(d => d.Id).ToListAsync();
                if (data.Count == 0)
                {
                    return NoContent();
                }
                return Ok(data);
            }
            catch (System.Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostRequestSuccess([FromBody] RequestSuccess request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                _context.RequestSuccesses.Add(request);
                await _context.SaveChangesAsync();
                return Ok(new {Message = $"Request success, 200, with {ModelState.IsValid}"});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostRequest([FromBody] Request request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                _context.Requests.Add(request);
                await _context.SaveChangesAsync();
                return Ok(new {Message = $"Request success, 200, with {ModelState.IsValid}"});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
        
        public async Task<IActionResult> GetWebDocuments()
        {
            try
            {
                var data = await _context.WebDocuments.Take(7).OrderByDescending(d => d.Id).ToListAsync();
                if (data.Count == 0)
                {
                    return NoContent();
                }
                return Ok(data);
            }
            catch (System.Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> POSTWebPage([FromBody] List<WebPage> WebDocuments)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                List<WebPage> webPagesList = new List<WebPage>();
                foreach (var item in WebDocuments)
                {
                    WebPage webPage = new WebPage()
                    {
                        Title = item.Title,
                        Url = item.Url,
                        CreatedAT = item.CreatedAT
                    };
                    webPagesList.Add(webPage);
                }                
                _context.WebDocuments.AddRange(webPagesList);
                await _context.SaveChangesAsync();
                
                await Task.Delay(2);
                DropDuplicateKeepRecords();
                var data = await _context.WebDocuments.Take(7).OrderByDescending(d => d.Id).ToListAsync();
                if (data.Count == 0)
                {
                    return NoContent();
                }
                return Ok(data);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private void DropDuplicateKeepRecords()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=resources_db;Integrated Security=True;";
                string delete = @"
                    WITH CTE AS (
                        SELECT 
                            Id, 
                            Title, 
                            Url,
                            CreatedAT,
                            ROW_NUMBER() OVER (PARTITION BY Title, Url ORDER BY CreatedAT) AS row_num
                        FROM 
                            WebDocuments
                    )
                    DELETE FROM WebDocuments
                    WHERE Id IN (
                        SELECT Id FROM CTE WHERE row_num > 1
                    );
                ";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(delete, connection);
                    command.ExecuteNonQuery();                    
                }
        }
        public async Task<IActionResult> GetWebPageBytopOrWhereOrContain(string topOrWhereOrContain, string indice)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(topOrWhereOrContain))
                {
                    return NotFound();
                }
                List<object> results = new List<object>();
                var query = await _context.WebDocuments.OrderByDescending(d => d.Id).Select(d => d.Title).ToListAsync();
                DropDuplicateKeepRecords();
                switch (topOrWhereOrContain)
                {
                    case "TOP":
                        var topData = await _context.WebDocuments.Take(int.Parse(indice.Trim())).OrderByDescending(d => d.Id).ToListAsync();
                        if(topData.Count == 0)
                        {
                            return NoContent();
                        }
                        return Ok(topData);
                    case "WHERE":
                        var whereData = await _context.WebDocuments.Where(d => d.Title == indice.Trim()).OrderByDescending(d => d.Id).ToListAsync();
                        if (whereData.Count == 0)
                        {
                            return NoContent();
                        }
                        return Ok(whereData);
                    case "CONTAIN":
                        var contain = await _context.WebDocuments.Where(d => d.Title.Contains($"%{indice.Trim()}%")).OrderByDescending(d => d.Id).ToListAsync();
                        if (contain.Count == 0)
                        {
                            return NoContent();
                        }
                        return Ok(contain);
                    default:
                        break;
                }
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> FetchWebDocuments()
        {
            try
            {
                var all = await _context.WebDocuments.OrderByDescending(d => d.Id).ToListAsync();
                if (all.Count == 0)
                {
                    return NoContent();
                }                        
                return Ok(all);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetDatasets()
        {
            try
            {
                var data = await _context.Datasets.Take(5).OrderByDescending(d => d.Id).ToListAsync();
                if (data.Count == 0)
                {
                    return NoContent();
                }
                return Ok(data);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetEndpoints()
        {
            try
            {
                var data = await _context.EndPoints.Take(5).OrderByDescending(d => d.Id).ToListAsync();
                if (data.Count == 0)
                {
                    return NoContent();
                }
                
                return Ok(data);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> SaveEndPoint(string url, string createdAT)
        {
            try
            {
                if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(createdAT))
                {
                    return NoContent();
                }

                EndPoint endPoint = new EndPoint()
                {
                    Url = url,
                    CreatedAT = createdAT,
                };
                
                _context.EndPoints.Add(endPoint);
                await _context.SaveChangesAsync();
                var data = await _context.EndPoints.Take(4).OrderByDescending(d => d.Id).ToListAsync();
                if (data.Count == 0)
                {
                    return NoContent();
                }
                return Ok(data);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> SaveDataset(string url, string address)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(address))
                {
                    return NotFound();
                }
                string fileName = Path.GetFileName(url);

                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    using (var reader = new StreamReader(stream))
                    {
                        // Read the content as CSV string
                        string csvContent = await reader.ReadToEndAsync();
                        
                        // Check if the content is not empty
                        if (!string.IsNullOrEmpty(csvContent))
                        {
                            // Write the content to a local CSV file
                            using (StreamWriter streamWriter = System.IO.File.CreateText(Path.Combine(@"" + address, fileName)))
                            {
                                streamWriter.WriteLine(csvContent);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Downloaded CSV content is empty.");
                        }
                    }
                }

                string localFileAddress = Path.Combine(address, fileName);
                if (string.IsNullOrWhiteSpace(localFileAddress) || !Path.GetFileName(localFileAddress).Contains(".csv"))
                {
                    return NotFound();
                }
                FileInfo fileInfo = null;
                fileInfo = new FileInfo(localFileAddress);
                Dataset dataset = new Dataset()
                {
                    FullAddress = localFileAddress,
                    Name = fileInfo.Name.ToString(),
                    LastWriteTime = fileInfo.LastWriteTime.ToString(),
                    LastAccessTime = fileInfo.LastAccessTime.ToString(),
                    DirectoryName = fileInfo.DirectoryName.ToString(),
                    Length = fileInfo.Length.ToString(),
                    CreatedAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                };
                _context.Datasets.Add(dataset);
                await _context.SaveChangesAsync();

                var data = await _context.Datasets.Where(d => d.Name == dataset.Name).ToListAsync();
                if (data.Count == 0)
                {
                    return NoContent();
                }
                return Ok(data);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }
        
        public static async Task<IEnumerable<UrlMetadata>> GetDataAsync(string url)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<IEnumerable<UrlMetadata>>(content);
                    return data; 
                }
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"HTTP request failed: {ex.Message}");
            }
            catch (JsonException ex)
            {
                throw new JsonException($"JSON serialization/deserialization failed: {ex.Message}");
            }
            catch (System.Exception ex)
            {
                throw new HttpRequestException($"HTTP request failed: {ex.Message}");
            } 
        }
        public async Task<IActionResult> EndPointPoster(string url)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(url))
                {
                    return NotFound();
                }
                EndPoint endPoint = new EndPoint()
                {
                    Url = url,
                    CreatedAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                };
                _context.Add(endPoint);
                await _context.SaveChangesAsync();
                
               return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> CountControllers()
        {
            var routes = await GetDataAsync("https://localhost/Endpoints");
            var data = routes.Select(d => d.ControllerName).Distinct().Count();
            return Ok(new { Count = data });
        }
        [HttpGet]
        public async Task<IActionResult> CountActionName()
        {
            var routes = await GetDataAsync("https://localhost/Endpoints");
            var data = routes.Select(d => d.ActionName).Distinct().Count();
            return Ok(new { Count = data });
        }
        public async Task<IActionResult> PostEndPoints()
        {
            try
            {
                var routes = await GetDataAsync("https://localhost/Endpoints");
                // Iterate over the routes and insert into database
                foreach (var route in routes)
                {
                    // Create an instance of EndpointMetadata
                    var endpointMetadata = new EndpointMetadata
                    {
                        ControllerName = route.ControllerName,
                        ActionName = route.ActionName,
                        HttpMethod = route.HttpMethod,
                        RouteTemplate = route.RouteTemplate
                    };

                    // Add the instance to the context for insertion
                    _context.UrlEndPoints.Add(endpointMetadata);
                }

                // Save changes to the database
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetByControllerName(string controllerName)
        {
            try
            {
                var routes = await GetDataAsync("https://localhost/Endpoints");
                if (string.IsNullOrWhiteSpace(controllerName))
                {
                    return NotFound();
                }

                var data = routes.Where(r => r.ControllerName == controllerName);
                if (routes.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(data);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetControllerName()
        {
            try
            {
                var routes = await GetDataAsync("https://localhost/Endpoints");
                if (routes.Count() == 0)
                {
                    return NoContent();
                }
                var data = routes.Select(d => d.ControllerName).Distinct();
                return Ok(data);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetByActionName(string controllerName, string actionName)
        {
            try
            {
                var routes = await GetDataAsync("https://localhost/Endpoints");
                if (string.IsNullOrWhiteSpace(actionName))
                {
                    return NotFound();
                }
                var data = routes.Where(r => r.ActionName == actionName && r.ControllerName == controllerName);
                if (routes.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(data);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetActionName(string controllerName)
        {
            try
            {
                var routes = await GetDataAsync("https://localhost/Endpoints");
                if (routes.Count() == 0)
                {
                    return NoContent();
                }
                var data = routes.Where(d => d.ControllerName == controllerName).Select(d => d.ActionName).Distinct();
                return Ok(data);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetCountsByHttpMethod(string controllerName, string httpMethod)
        {
            try
            {
                var routes = await GetDataAsync("https://localhost/Endpoints");
                if (string.IsNullOrWhiteSpace(httpMethod))
                {
                    return NotFound();
                }
                var data = routes.Where(r => r.HttpMethod == httpMethod && r.ControllerName == controllerName);
                if (routes.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(new {Counts = data.Count()});
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetByHttpMethod(string controllerName, string httpMethod)
        {
            try
            {
                var routes = await GetDataAsync("https://localhost/Endpoints");
                if (string.IsNullOrWhiteSpace(httpMethod))
                {
                    return NotFound();
                }
                var data = routes.Where(r => r.HttpMethod == httpMethod && r.ControllerName == controllerName);
                if (routes.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(data);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetHttpMethod(string controllerName)
        {
            try
            {
                var routes = await GetDataAsync("https://localhost/Endpoints");
                if (routes.Count() == 0)
                {
                    return NoContent();
                }
                var data = routes.Where(d => d.ControllerName == controllerName).Select(d => d.HttpMethod).Distinct();
                return Ok(data);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetByRouteTemplate(string routeTemplate)
        {
            try
            {
                var routes = await GetDataAsync("https://localhost/Endpoints");
                if (string.IsNullOrWhiteSpace(routeTemplate))
                {
                    return NotFound();
                }
                var data = routes.Where(r => r.RouteTemplate == routeTemplate);
                if (routes.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(data);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetRouteTemplate(string controllerName)
        {
            try
            {
                var routes = await GetDataAsync("https://localhost/Endpoints");
                if (routes.Count() == 0)
                {
                    return NoContent();
                }
                var data = routes.Where(d => d.ControllerName == controllerName).Select(d => d.RouteTemplate).Distinct();
                return Ok(data);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // GET: UrlEndPoints
        public async Task<IActionResult> Index()
        {
            return View(await _context.UrlEndPoints.Take(10).ToListAsync());
        }

        // GET: UrlEndPoints/DetailsWebPage/5
        public async Task<IActionResult> DetailsWebPage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var WebPage = await _context.WebDocuments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (WebPage == null)
            {
                return NotFound();
            }

            return View(WebPage);
        }
        
        // GET: UrlEndPoints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endpointMetadata = await _context.UrlEndPoints
                .FirstOrDefaultAsync(m => m.Id == id);
            if (endpointMetadata == null)
            {
                return NotFound();
            }

            return View(endpointMetadata);
        }

        // GET: UrlEndPoints/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UrlEndPoints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ControllerName,ActionName,HttpMethod,RouteTemplate")] EndpointMetadata endpointMetadata)
        {
            if (ModelState.IsValid)
            {
                _context.Add(endpointMetadata);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(endpointMetadata);
        }

        // GET: UrlEndPoints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endpointMetadata = await _context.UrlEndPoints.FindAsync(id);
            if (endpointMetadata == null)
            {
                return NotFound();
            }
            return View(endpointMetadata);
        }
        

        // POST: UrlEndPoints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ControllerName,ActionName,HttpMethod,RouteTemplate")] EndpointMetadata endpointMetadata)
        {
            if (id != endpointMetadata.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(endpointMetadata);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EndpointMetadataExists(endpointMetadata.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(endpointMetadata);
        }
        // GET: UrlEndPoints/EditWebPage/5
        public async Task<IActionResult> EditWebPage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webPage = await _context.WebDocuments.FindAsync(id);
            if (webPage == null)
            {
                return NotFound();
            }
            return View(webPage);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditWebPage(int id, [Bind("Id,Title,Url,CreatedAT")] WebPage webPage)
        {
            if (id != webPage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(webPage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebPageExists(webPage.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(webPage);
        }

        // GET: UrlEndPoints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endpointMetadata = await _context.UrlEndPoints
                .FirstOrDefaultAsync(m => m.Id == id);
            if (endpointMetadata == null)
            {
                return NotFound();
            }

            return View(endpointMetadata);
        }

        // POST: UrlEndPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var endpointMetadata = await _context.UrlEndPoints.FindAsync(id);
            _context.UrlEndPoints.Remove(endpointMetadata);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: UrlEndPoints/DeleteWebPage/5
        public async Task<IActionResult> DeleteWebPage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var WebDocuments = await _context.WebDocuments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (WebDocuments == null)
            {
                return NotFound();
            }

            return View(WebDocuments);
        }

        // POST: UrlEndPoints/DeleteWebPage/5
        [HttpPost, ActionName("DeleteWebPage")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWebPageConfirmed(int id)
        {
            var WebDocuments = await _context.WebDocuments.FindAsync(id);
            _context.WebDocuments.Remove(WebDocuments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EndpointMetadataExists(int id)
        {
            return _context.UrlEndPoints.Any(e => e.Id == id);
        }
        // WebPageExists
        private bool WebPageExists(int id)
        {
            return _context.WebDocuments.Any(e => e.Id == id);
        }
    }
}
