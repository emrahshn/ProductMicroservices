using Microsoft.EntityFrameworkCore;
using Review.Products.Entities;

namespace Review.Products.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; set; }
        Task<int> SaveChanges();

    }
}
