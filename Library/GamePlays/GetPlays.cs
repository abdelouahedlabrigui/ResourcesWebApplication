using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ResourcesWebApplication.Library.GamePlays.Models;

namespace ResourcesWebApplication.Library
{
    public class GetPlays
    {
        public async Task<IEnumerable<GamePlayersDataset>> GetPlaysAsync(string url)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<IEnumerable<GamePlayersDataset>>(content);
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
    }
}