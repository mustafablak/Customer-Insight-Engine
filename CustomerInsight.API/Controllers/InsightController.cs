using Microsoft.AspNetCore.Mvc;
using CustomerInsight.API.Services;
using CustomerInsight.API.Data;
using CustomerInsight.API.Models;

namespace CustomerInsight.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsightController : ControllerBase
    {
        private readonly AIApiService _aiService;
        private readonly AppDbContext _context;

        public InsightController(AIApiService aiService, AppDbContext context)
        {
            _aiService = aiService;
            _context = context;
        }

        [HttpPost("analyze")]
        public async Task<IActionResult> AnalyzeReview([FromBody] string reviewText)
        {
            if (string.IsNullOrWhiteSpace(reviewText))
                return BadRequest("Yorum metni boş olamaz.");

            // İŞTE BURAYI DÜZELTTİK: Servisindeki doğru metot ismini (AnalyzeReviewAsync) çağırıyoruz
            var aiResponse = await _aiService.AnalyzeReviewAsync(reviewText);

            if (aiResponse == null)
                return StatusCode(500, "Yapay zeka servisiyle iletişim kurulamadı.");

            // 2. Gelen cevabı SQL'e kaydetmek üzere veritabanı modelimize (Entity) dönüştür
            var newReview = new CustomerReview
            {
                ReviewText = aiResponse.Review_Text, // Not: Eğer modelinde 'ReviewText' yazıyorsa alt çizgiyi kaldırabilirsin
                Sentiment = aiResponse.Sentiment,
                Confidence = aiResponse.Confidence,
                CreatedAt = DateTime.Now
            };

            // 3. Modeli veritabanına ekle ve kaydet
            _context.Reviews.Add(newReview);
            await _context.SaveChangesAsync();

            // 4. Veritabanına başarıyla kaydedilen nesneyi (Id'si ile birlikte) kullanıcıya göster
            return Ok(newReview);
        }
    }
}