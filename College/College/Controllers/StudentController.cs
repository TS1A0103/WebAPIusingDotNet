using College.Data;
using College.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace College.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly CollegeDBContext _dbContext;
        public StudentController(ILogger<StudentController> logger, CollegeDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        //endpoint for getting all details
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("All", Name = "GetAllStudents")]

        public ActionResult<IEnumerable<StudentDTO>> GetStudents()

        {
            //var students = new List<StudentDTO>();
            //foreach (var item in CollegeRepository.Students)
            //{
            //    StudentDTO obj = new StudentDTO()
            //    {
            //        Id = item.Id,
            //        StudentName = item.StudentName,
            //        Address = item.Address,
            //        Email = item.Email
            //    };
            //    students.Add(obj);
            //}
            _logger.LogInformation("GetStudents method got started executing");

            var students = _dbContext.Students.ToList();
            
            //var students = _dbContext.Students.Select(s => new StudentDTO()
            //{
            //    Id = s.Id,
            //    StudentName = s.StudentName,
            //    Address = s.Address,
            //    Email = s.Email,
            //    DOB = s.DOB,

            //}).ToList();
               
            //Ok-200 -success
            return Ok(students);
        }

        // GET: api/Student/1  (ONLY matches ints)
        [HttpGet("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            //BadRequest - 400 - Badrequest - client error
            if (id <= 0) 
            {
                _logger.LogWarning("BadRequest");
                return BadRequest();
            }
                
            var student=(_dbContext.Students.Where(n => n.Id == id).FirstOrDefault());
            //NotFound - 404 - NotFound -client error
            if (student == null)
            {
                _logger.LogError("Student not found with Id");
                return NotFound($"Student with '{id}' not found");
            }
            var StudentDTO = new StudentDTO()
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address,
                DOB= student.DOB,
            };
            //Ok - 200 -Success
            return Ok(StudentDTO);
        }

        // GET: api/Student/byname/john
        [HttpGet("byname/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentByName(string name)
        {
            
            var student = _dbContext.Students.FirstOrDefault(s => s.StudentName == name);
            if (student == null) return NotFound($"Student with name '{name}' not found");
            var StudentDTO = new StudentDTO()
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address,
                DOB= student.DOB,
            };
            return Ok(StudentDTO);
        }
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model)
        {
            if (model == null)
            { return BadRequest(); }
            //if (model.AdnissionDate < DateTime.Now)
            //{
            //    //1. Directly adding error message to modelstate
            //    //2Using Custom attribute
            //    ModelState.AddModelError("Admission date error", "Admission date must be greated than on equal to todays date");
            //    return BadRequest(ModelState);
            //}
            
            Student student = new Student
            {
                StudentName = model.StudentName,
                Address = model.Address,
                Email = model.Email
            };
            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();
            model.Id = student.Id;
            //Status 201 - to created url along with returning new student details
            return CreatedAtRoute("GetStudentById", new { id = model.Id }, model);
            //return Ok(model);
        }

        //HttpPut request to update the record
        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult UpdateStudent([FromBody] Student model)
        {
            if(model==null || model.Id <=0)
                return BadRequest();

            var existingStudent = _dbContext.Students.AsNoTracking().Where(s => s.Id == model.Id).FirstOrDefault();

            if(existingStudent ==null)
                return NotFound();

            var newRecord = new Student()
            {
                Id = existingStudent.Id,
                StudentName = model.StudentName,
                Email = model.Email,
                Address = model.Address,
                DOB = model.DOB
            };
            _dbContext.Students.Update(newRecord);

            
            //existingStudent.StudentName = model.StudentName;
            //existingStudent.Email = model.Email;
            //existingStudent.Address = model.Address;
            //existingStudent.DOB = model.DOB;

            _dbContext.SaveChanges();

            return NoContent();

        }
        //HttpPatch request to update the record partially
        [HttpPatch]
        [Route("{id:int}UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult UpdateStudentPartial(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
                return BadRequest();

            var existingStudent = _dbContext.Students.Where(s => s.Id == id).FirstOrDefault();

            if (existingStudent == null)
                return NotFound();

            var StudentDTO = new StudentDTO
            {
                Id = existingStudent.Id,
                StudentName = existingStudent.StudentName,
                Email = existingStudent.Email,
                Address = existingStudent.Address,
                DOB = existingStudent.DOB,
            };
            patchDocument.ApplyTo(StudentDTO, ModelState);
            if(!ModelState.IsValid)
                return BadRequest();

            existingStudent.StudentName = StudentDTO.StudentName;
            existingStudent.Email = StudentDTO.Email;
            existingStudent.Address = StudentDTO.Address;
            existingStudent.DOB = StudentDTO.DOB;

            _dbContext.SaveChanges();
            return NoContent();

        }

        // DELETE: api/Student/1
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteStudent(int id)
        {
            var student = _dbContext.Students.FirstOrDefault(s => s.Id == id);
            if (student == null) return NotFound($"Student with id {id} not found");

            _dbContext.Students.Remove(student);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
