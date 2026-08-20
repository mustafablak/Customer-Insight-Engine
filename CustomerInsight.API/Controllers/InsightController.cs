using Microsoft.AspNetCore.Mvc;
using CustomerInsight.API.Services;

namespace CustomerInsight.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsightController : ControllerBase
    {
        private readonly AIApiService _aiApiService;
        public InsightController(AIApiService aiApiService)
        {
            _aiApiService = aiApiService;
        }

        [HttpPost("analyze")]
        public async Task<IActionResult> AnalyzeCustomerReview([FromBody] string reviewText)
        {
            if (string.IsNullOrWhiteSpace(reviewText))
                return BadRequest("Yorum metni boş olamaz.");

            var result = await _aiApiService.AnalyzeReviewAsync(reviewText);

            if (result == null)
                return StatusCode(500, "Yapay zeka servisine ulaşılamadı. Python sunucusunun açık olduğundan emin olun.");

            return Ok(result);
        }
    }
}