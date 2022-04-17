using CDA.Models;
using Microsoft.EntityFrameworkCore;

namespace CDA.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
        public DbSet<User> users { get; set; }
        public DbSet<CriminalCode> criminalCodes { get; set; }
        public DbSet<Status> status { get; set; }
    }
}
