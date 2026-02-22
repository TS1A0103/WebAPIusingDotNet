using AutoMapper;
using College.Data;
using College.Data.Repository;
using College.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;


namespace College.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;
        public StudentController(ILogger<StudentController> logger, IMapper mapper, IStudentRepository studentRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _studentRepository = studentRepository;
        }
        //endpoint for getting all details
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("All", Name = "GetAllStudents")]

        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()

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
            var students = await _studentRepository.GetAllAsync();

            var studentDTOData = _mapper.Map<List<StudentDTO>>(students);

            //Ok-200 -success
            return Ok(studentDTOData);
        }

        // GET: api/Student/1  (ONLY matches ints)
        [HttpGet("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO>> GetStudentByIdAsync(int id)
        {
            //BadRequest - 400 - Badrequest - client error
            if (id <= 0) 
            {
                _logger.LogWarning("BadRequest");
                return BadRequest();
            }
                
            var student=await _studentRepository.GetByIdAsync(id);
            //NotFound - 404 - NotFound -client error
            if (student == null)
            {
                _logger.LogError("Student not found with Id");
                return NotFound($"Student with '{id}' not found");
            }
            var StudentDTO = _mapper.Map<StudentDTO>(student);
            //Ok - 200 -Success
            return Ok(StudentDTO);
        }

        // GET: api/Student/byname/john
        [HttpGet("byname/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO>> GetStudentByNameAsync(string name)
        {
            
            var student =await _studentRepository.GetByNameAsync(name);
            if (student == null) return NotFound($"Student with name '{name}' not found");
            var StudentDTO = _mapper.Map<StudentDTO>(student);
            return Ok(StudentDTO);
        }
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO>> CreateStudentAsync([FromBody] StudentDTO dto)
        {
            if (dto == null)
            { return BadRequest(); }
            //if (model.AdnissionDate < DateTime.Now)
            //{
            //    //1. Directly adding error message to modelstate
            //    //2Using Custom attribute
            //    ModelState.AddModelError("Admission date error", "Admission date must be greated than on equal to todays date");
            //    return BadRequest(ModelState);
            //}

            Student student = _mapper.Map<Student>(dto);

            var id = await _studentRepository.CreateAsync(student);
            
            dto.Id = id;
            //Status 201 - to created url along with returning new student details
            return CreatedAtRoute("GetStudentById", new { id = dto.Id }, dto);
            //return Ok(model);
        }

        //HttpPut request to update the record
        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> UpdateStudentAsync([FromBody] StudentDTO dto)
        {
            if(dto==null || dto.Id <=0)
                return BadRequest();

            var existingStudent =await _studentRepository.GetByIdAsync(dto.Id, true);

            if(existingStudent ==null)
                return NotFound();

            var newRecord = _mapper.Map<Student>(dto);
            await _studentRepository.UpdateAsync(newRecord);


            //existingStudent.StudentName = model.StudentName;
            //existingStudent.Email = model.Email;
            //existingStudent.Address = model.Address;
            //existingStudent.DOB = model.DOB;

            

            return NoContent();

        }
        //HttpPatch request to update the record partially
        [HttpPatch]
        [Route("{id:int}UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> UpdateStudentPartialAsync(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
                return BadRequest();

            var existingStudent =await _studentRepository.GetByIdAsync(id, true);

            if (existingStudent == null)
                return NotFound();

            var StudentDTO = _mapper.Map<StudentDTO>(existingStudent);
            patchDocument.ApplyTo(StudentDTO, ModelState);
            if(!ModelState.IsValid)
                return BadRequest();

            existingStudent = _mapper.Map<Student>(StudentDTO);

            await _studentRepository.UpdateAsync(existingStudent);

            
            return NoContent();

        }

        // DELETE: api/Student/
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteStudentAsync(int id)
        {
            var student =await _studentRepository.GetByIdAsync(id);
            if (student == null) return NotFound($"Student with id {id} not found");

            await _studentRepository.DeleteAsync(student)  ;
            return NoContent();
        }
    }
}
