using Microsoft.EntityFrameworkCore;
using Review.Votes.Entities;

namespace Review.Votes.Data
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Votes.Any())
                {
                    return;   //Data was already seeded
                }

                context.Votes.AddRange(
                    new Vote()
                    {
                        Id = 1,
                        UserId = 1,
                        ProductId = 1,
                        Value = 3,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                    },
                    new Vote()
                    {
                        Id = 2,
                        UserId = 1,
                        ProductId = 1,
                        Value = 3,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                    },
                    new Vote()
                    {
                        Id = 3,
                        UserId = 2,
                        ProductId = 1,
                        Value = 4,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                    },
                    new Vote()
                    {
                        Id = 4,
                        UserId = 2,
                        ProductId = 2,
                        Value = 5,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                    },
                    new Vote()
                    {
                        Id = 5,
                        UserId = 2,
                        ProductId = 2,
                        Value = 3,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                    },
                    new Vote()
                    {
                        Id = 6,
                        UserId = 3,
                        ProductId = 3,
                        Value = 2,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                    },
                    new Vote()
                    {
                        Id = 7,
                        UserId = 3,
                        ProductId = 3,
                        Value = 3,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                    },
                    new Vote()
                    {
                        Id = 8,
                        UserId = 3,
                        ProductId = 3,
                        Value = 1,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                    });

                context.SaveChanges();
            }
        }
    }
}
