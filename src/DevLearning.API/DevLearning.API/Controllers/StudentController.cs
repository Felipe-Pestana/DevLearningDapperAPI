using DevLearning.API.Models.DTOs.Student;
using DevLearning.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpPost()]
        public async Task CreateStudent([FromBody] StudentRequestDTO student)
        {
            try
            {
                await _studentService.CreateStudent(student);
                Created();
            } catch (Exception ex)
            {
                StatusCode(500, new { error = ex.Message }); 
            }
        }
    }
}
