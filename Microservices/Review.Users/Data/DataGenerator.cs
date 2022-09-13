using Microsoft.EntityFrameworkCore;
using Review.Users.Entities;

namespace Review.Users.Data
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Users.Any())
                {
                    return;   //Data was already seeded
                }

                context.Users.AddRange(
                    new User()
                    {
                        Id = 1,
                        Name = "Athos",
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                    },
                    new User()
                    {
                        Id = 2,
                        Name = "Porthos",
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                    },
                    new User()
                    {
                        Id = 3,
                        Name = "Aramis",
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                    });

                context.SaveChanges();
            }
        }
    }
}
