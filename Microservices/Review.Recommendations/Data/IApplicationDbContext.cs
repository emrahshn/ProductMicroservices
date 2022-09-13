using Microsoft.EntityFrameworkCore;
using Review.Recommendations.Entities;

namespace Review.Recommendations.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Recommendation> Recommendations { get; set; }
        Task<int> SaveChanges();

    }
}
