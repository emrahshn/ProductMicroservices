using Microsoft.EntityFrameworkCore;
using Review.Products.Entities;

namespace Review.Products.Data
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Products.Any())
                {
                    return;   //Data was already seeded
                }

                context.Products.AddRange(
                    new Product()
                    {
                        Id = 1,
                        Name = "Westmalle Extra",
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                    },
                    new Product()
                    {
                        Id = 2,
                        Name = "La Trappe Dubbel",
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                    },
                    new Product()
                    {
                        Id = 3,
                        Name = "Maredsous Blonde",
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                    });

                context.SaveChanges();
            }
        }
    }
}
