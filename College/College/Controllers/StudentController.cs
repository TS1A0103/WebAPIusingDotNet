using College.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace College.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        //endpoint
        [HttpGet]
        public IEnumerable<Student> GetStudents()
        {
            return new List<Student>{
                new Student
            {
                Id = 1,
                StudentName = "Student 1",
                Email = "student1@gmail.com",
                Address = "Hyd, India"
            },
            new Student
            {
                Id = 2,
                StudentName = "Student 2",
                Email = "student2@gmail.com",
                Address = "Vij, India"
            }
            }
            ;
        }
    }
}
