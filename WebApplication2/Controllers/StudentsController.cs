using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        
        // GET
        public string GetStudents()
        {
            return "Jan,Adam";
        }
    }
}