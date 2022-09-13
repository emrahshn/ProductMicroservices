using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Review.Votes.Data;
using Review.Votes.Entities;

namespace Review.Votes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private IApplicationDbContext _context;
        public VoteController(IApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Create(Vote vote)
        {
            _context.Votes.Add(vote);
            await _context.SaveChanges();
            return Ok(vote.Id);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()

        {
            var votes = await _context.Votes.ToListAsync();
            if (votes == null) return NotFound();
            return Ok(votes);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vote = await _context.Votes.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (vote == null) return NotFound();
            return Ok(vote);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var vote = await _context.Votes.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (vote == null) return NotFound();
            vote.IsDeleted = true;
            _context.Votes.Update(vote);
            await _context.SaveChanges();
            return Ok(vote.Id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Vote voteData)
        {
            var vote = _context.Votes.Where(a => a.Id == id).FirstOrDefault();

            if (vote == null) return NotFound();
            else
            {
                vote.UserId = voteData.UserId;
                vote.ProductId = voteData.ProductId;
                vote.Value = voteData.Value;
                vote.IsDeleted = voteData.IsDeleted;
                vote.IsActive = voteData.IsActive;
                vote.CreatedAt = voteData.CreatedAt;
                await _context.SaveChanges();
                return Ok(vote.Id);
            }
        }
        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetByProductId(int id)
        {
            var vote = await _context.Votes.Where(a => a.ProductId == id).ToListAsync();
            if (vote == null) return NotFound();
            return Ok(vote);
        }
    }
}
