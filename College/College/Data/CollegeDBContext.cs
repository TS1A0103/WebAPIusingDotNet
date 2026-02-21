using Microsoft.EntityFrameworkCore;

namespace College.Data
{
    public class CollegeDBContext : DbContext
    {
        public CollegeDBContext(DbContextOptions<CollegeDBContext> options) : base(options)
        {
            
        }
        DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(new List<Student>()
            {
                new Student() {
                    Id = 1,
                    StudentName = "Vamsi",
                    Address= "India",
                    Email = "Vamsi@gmail.com",
                    DOB = new DateTime (1999,12,12)
                },
                new Student() {
                    Id = 2,
                    StudentName = "Venkat",
                    Address= "India",
                    Email = "Venkat@gmail.com",
                    DOB = new DateTime (2000,12,12)
                },
            });
        }
    }
}
