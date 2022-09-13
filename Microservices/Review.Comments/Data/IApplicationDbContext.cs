using Microsoft.EntityFrameworkCore;
using Review.Comments.Entities;

namespace Review.Comments.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Comment> Comments { get; set; }
        Task<int> SaveChanges();
    }
}
