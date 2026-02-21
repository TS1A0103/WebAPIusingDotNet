using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    { 
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.StudentName).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Address).IsRequired(false).HasMaxLength(500);

            builder.HasData(new List<Student>()
            {
                new Student()
                {
                    Id = 1,
                    StudentName = "Vamsi",
                    Address = "India",
                    Email = "Vamsi@gmail.com",
                    DOB = new DateTime(1999, 12, 12)
                },
                new Student()
                {
                    Id = 2,
                    StudentName = "Venkat",
                    Address = "India",
                    Email = "Venkat@gmail.com",
                    DOB = new DateTime(2000, 12, 12)
                }
            });
               
        }
    }
}
