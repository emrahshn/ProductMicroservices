using Microsoft.EntityFrameworkCore;
using Review.Recommendations.Entities;

namespace Review.Recommendations.Data
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Recommendations.Any())
                {
                    return;   //Data was already seeded
                }

                context.Recommendations.AddRange(
                    new Recommendation()
                    {
                        Id = 1,
                        UserId = 1,
                        RecommendedTo = "leonardo@davinci.com",
                        ProductId = 1,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                    },
                    new Recommendation()
                    {
                        Id = 2,
                        UserId = 2,
                        RecommendedTo = "pablo@picasso.com",
                        ProductId = 2,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                    },
                    new Recommendation()
                    {
                        Id = 3,
                        UserId = 3,
                        RecommendedTo = "claude@monet.com",
                        ProductId = 2,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                    },
                    new Recommendation()
                    {
                        Id = 4,
                        UserId = 1,
                        RecommendedTo = "michelangelo@buonarrotti.com",
                        ProductId = 2,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                    },
                    new Recommendation()
                    {
                        Id = 5,
                        UserId = 2,
                        RecommendedTo = "salvador@dali.com",
                        ProductId = 2,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                    },
                    new Recommendation()
                    {
                        Id = 6,
                        UserId = 3,
                        RecommendedTo = "vincent@vangogh.com",
                        ProductId = 3,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                    });

                context.SaveChanges();
            }
        }
    }
}
