using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Review.Users.Data;
using Review.Users.Entities;

namespace Review.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IApplicationDbContext _context;
        public UserController(IApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChanges();
            return Ok(user.Id);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()

        {
            var users = await _context.Users.ToListAsync();
            if (users == null) return NotFound();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _context.Users.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (user == null) return NotFound();
            return Ok(user);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (user == null) return NotFound();
            user.IsDeleted = true;
            _context.Users.Update(user);
            await _context.SaveChanges();
            return Ok(user.Id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User userData)
        {
            var user = _context.Users.Where(a => a.Id == id).FirstOrDefault();

            if (user == null) return NotFound();
            else
            {
                user.Name = userData.Name;
                user.IsDeleted = userData.IsDeleted;
                user.IsActive = userData.IsActive;
                user.CreatedAt = userData.CreatedAt;
                await _context.SaveChanges();
                return Ok(user.Id);
            }
        }
    }
}
