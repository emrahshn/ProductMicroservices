using Microsoft.EntityFrameworkCore;
using Review.Users.Entities;

namespace Review.Users.Data
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }
        Task<int> SaveChanges();
    }
}
