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
        public string GetStudentName()
        {
            return  "Student name 1";
        }
    }
}
