using System.Text;
using System.Text.Json;
using CustomerInsight.API.Models;

namespace CustomerInsight.API.Services
{
    public class AIApiService
    {
        private readonly HttpClient _httpClient;

        public AIApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://127.0.0.1:8000/"); 
        }

        public async Task<SentimentResponse?> AnalyzeReviewAsync(string reviewText)
        {
            var requestModel = new SentimentRequest { Text = reviewText };
            
            // BURAYA DİKKAT: JSON dönüştürücüsüne ilk harfi küçük yapmasını söylüyoruz (CamelCase)
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var jsonContent = new StringContent(JsonSerializer.Serialize(requestModel, options), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("analyze-sentiment/", jsonContent);
            
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<SentimentResponse>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result;
            }

            return null; 
        }
    }
}