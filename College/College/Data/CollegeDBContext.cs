using College.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace College.Data
{
    public class CollegeDBContext : DbContext
    {
        public CollegeDBContext(DbContextOptions<CollegeDBContext> options) : base(options)
        {
            
        }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //1.Table
            modelBuilder.ApplyConfiguration(new StudentConfig());
            //2.Table

        }
    }
}
