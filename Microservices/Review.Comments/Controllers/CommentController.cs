using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Review.Comments.Data;
using Review.Comments.Entities;

namespace Review.Comments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private IApplicationDbContext _context;
        public CommentController(IApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Create(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChanges();
            return Ok(comment.Id);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        
        {
            var comments = await _context.Comments.ToListAsync();
            if (comments == null) return NotFound();
            return Ok(comments);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comment = await _context.Comments.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (comment == null) return NotFound();
            return Ok(comment);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _context.Comments.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (comment == null) return NotFound();
            comment.IsDeleted = true;
            _context.Comments.Update(comment);
            await _context.SaveChanges();
            return Ok(comment.Id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Comment commentData)
        {
            var comment = _context.Comments.Where(a => a.Id == id).FirstOrDefault();

            if (comment == null) return NotFound();
            else
            {
                comment.Description = commentData.Description;
                comment.UserId = commentData.UserId;
                comment.IsDeleted = commentData.IsDeleted;
                comment.IsActive = commentData.IsActive;
                comment.CreatedAt = commentData.CreatedAt;
                await _context.SaveChanges();
                return Ok(comment.Id);
            }
        }
        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetByProductId(int id)
        {
            var comment = await _context.Comments.Where(a => a.ProductId == id).ToListAsync();
            if (comment == null) return NotFound();
            return Ok(comment);
        }
    }
}
