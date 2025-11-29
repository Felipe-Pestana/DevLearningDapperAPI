using DevLearning.Api.Models.Dtos;
using DevLearning.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevLearning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public StudentService _service;

        public StudentController(StudentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> CreateStudentAsync(CreateStudentDto student)
        {
            try
            {
                await _service.CreateStudentAsync(student);
                return Created();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<StudentResponseDto>>> GetAllStudentsAsync()
        {
            try
            {
                var students = await _service.GetAllStudentsAsync();
                var message = "Lista Vazia!";

                if (students is null)
                    return NotFound(message);

                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<StudentResponseDto>>> GetStudentByIdAsync(Guid id)
        {
            try
            {
                var student = await _service.GetStudentByIdAsync(id);
                var message = "Estudante não encontrado!";

                if (student is null)
                    return NotFound(message);

                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudentAsync(Guid id, UpdateStudentDto student)
        {
                
            var message = "Estudante não encontrado!";
            var studentExist = await _service.GetStudentByIdAsync(id);
            if (studentExist is null)
                return NotFound(message);

            try
            {                    
                await _service.UpdateStudentAsync(id, student);

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public async Task<ActionResult> DeleteStudentAsync(Guid id)
        {
            var message = "Estudante não encontrado!";
            var studentExist = await _service.GetStudentByIdAsync(id);
            if (studentExist is null)
                return NotFound(message);

            try
            {
                await _service.DeleteStudentAsync(id);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
