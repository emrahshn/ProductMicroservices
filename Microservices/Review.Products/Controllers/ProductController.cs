using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Review.Products.Data;
using Review.Products.Entities;

namespace Review.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IApplicationDbContext _context;
        public ProductController(IApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChanges();
            return Ok(product.Id);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()

        {
            var products = await _context.Products.ToListAsync();
            if (products == null) return NotFound();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (product == null) return NotFound();
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (product == null) return NotFound();
            product.IsDeleted = true;
            _context.Products.Update(product);
            await _context.SaveChanges();
            return Ok(product.Id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product productData)
        {
            var product = _context.Products.Where(a => a.Id == id).FirstOrDefault();

            if (product == null) return NotFound();
            else
            {
                product.Name = productData.Name;
                product.IsDeleted = productData.IsDeleted;
                product.IsActive = productData.IsActive;
                product.CreatedAt = productData.CreatedAt;
                await _context.SaveChanges();
                return Ok(product.Id);
            }
        }
    }
}
