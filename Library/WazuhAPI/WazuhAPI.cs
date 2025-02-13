using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
namespace ResourcesWebApplication.Library.WazuhAPI
{
    public class WazuhAPI
    {
        private readonly string _apiUrl;
        private readonly string _username;
        private readonly string _password;
        private readonly string _token;
        private readonly HttpClient _client;

        public WazuhAPI(string apiUrl, string username, string password)
        {
            _apiUrl = apiUrl;
            _username = username;
            _password = password;
            _client = new HttpClient();
            _token = GetToken().GetAwaiter().GetResult();

            if (!string.IsNullOrEmpty(_token))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            else
            {
                throw new Exception("Failed to obtain token.");
            }
        }

        private async Task<string> GetToken()
        {
            var url = $"{_apiUrl}/security/user/authenticate";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var byteArray = new System.Text.UTF8Encoding().GetBytes($"{_username}:{_password}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(jsonString);
                return json["data"]["token"].ToString();
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception($"HTTP error occurred: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Other error occurred: {ex.Message}");
            }
        }

        public async Task<JObject> GetAgents()
        {
            if (string.IsNullOrEmpty(_token))
            {
                throw new Exception("Authentication failed. No token available.");
            }

            var url = $"{_apiUrl}/agents";
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return JObject.Parse(jsonString);
        }        
    }
}
