using College.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace College.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        //endpoint for getting all details
        [HttpGet]
        [Route("All", Name = "GetAllStudents")]

        public IEnumerable<Student> GetStudents()
        {
            return CollegeRepository.Students;
        }

        // GET: api/Student/1  (ONLY matches ints)
        [HttpGet("{id:int}")]
        public Student GetStudentById(int id)
        {
            return CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
        }

        // GET: api/Student/byname/john
        [HttpGet("byname/{name}")]
        public ActionResult<Student> GetStudentByName(string name)
        {
            var student = CollegeRepository.Students.FirstOrDefault(s => s.StudentName == name);
            if (student == null) return NotFound($"Student with name '{name}' not found");
            return Ok(student);
        }

        // DELETE: api/Student/1
        [HttpDelete("{id:int}")]
        public ActionResult DeleteStudent(int id)
        {
            var student = CollegeRepository.Students.FirstOrDefault(s => s.Id == id);
            if (student == null) return NotFound($"Student with id {id} not found");

            CollegeRepository.Students.Remove(student);
            return NoContent();
        }
    }
}
