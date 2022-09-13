using Microsoft.EntityFrameworkCore;
using Review.Votes.Entities;

namespace Review.Votes.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Vote> Votes { get; set; }
        Task<int> SaveChanges();
    }
}
