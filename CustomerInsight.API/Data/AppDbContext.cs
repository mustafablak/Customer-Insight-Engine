using Microsoft.EntityFrameworkCore;
using CustomerInsight.API.Models;

namespace CustomerInsight.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // SQL'de oluşacak tablonun adı "Reviews" olacak
        public DbSet<CustomerReview> Reviews { get; set; } 
    }
}