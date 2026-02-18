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

        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            //Ok-200 -success
            return Ok(CollegeRepository.Students);
        }

        // GET: api/Student/1  (ONLY matches ints)
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Student> GetStudentById(int id)
        {
            //BadRequest - 400 - Badrequest - client error
            if (id <= 0)
                return BadRequest();
            var student=(CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault());
            //NotFound - 404 - NotFound -client error
            if (student == null) return NotFound($"Student with '{id}' not found");
            //Ok - 200 -Success
            return Ok(student);
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
