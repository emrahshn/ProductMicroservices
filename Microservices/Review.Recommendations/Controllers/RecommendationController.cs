using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Review.Recommendations.Data;
using Review.Recommendations.Entities;

namespace Review.Recommendations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private IApplicationDbContext _context;
        public RecommendationController(IApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Create(Recommendation recommendation)
        {
            _context.Recommendations.Add(recommendation);
            await _context.SaveChanges();
            return Ok(recommendation.Id);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()

        {
            var recommendations = await _context.Recommendations.ToListAsync();
            if (recommendations == null) return NotFound();
            return Ok(recommendations);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var recommendation = await _context.Recommendations.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (recommendation == null) return NotFound();
            return Ok(recommendation);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var recommendation = await _context.Recommendations.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (recommendation == null) return NotFound();
            recommendation.IsDeleted = true;
            _context.Recommendations.Update(recommendation);
            await _context.SaveChanges();
            return Ok(recommendation.Id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Recommendation recommendationData)
        {
            var recommendation = _context.Recommendations.Where(a => a.Id == id).FirstOrDefault();

            if (recommendation == null) return NotFound();
            else
            {
                recommendation.RecommendedTo = recommendationData.RecommendedTo;
                recommendation.ProductId = recommendationData.ProductId;
                recommendation.IsDeleted = recommendationData.IsDeleted;
                recommendation.IsActive = recommendationData.IsActive;
                recommendation.CreatedAt = recommendationData.CreatedAt;
                await _context.SaveChanges();
                return Ok(recommendation.Id);
            }
        }

        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetByProductId(int id)
        {
            var recommendation = await _context.Recommendations.Where(a => a.ProductId == id).ToListAsync();
            if (recommendation == null) return NotFound();
            return Ok(recommendation);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            var recommendationCount = await _context.Recommendations.CountAsync();
            return Ok(recommendationCount);
        }
    }
}
