using API.Controllers.Interfaces;
using API.Models.DTOs.Student;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase, IStudentController
    {
        private StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("HeartBeat")]
        public ActionResult<string> HearBeat()
        {
            return Ok("StudentController funcionando corretamente!");
        }

        [HttpPost]
        public async Task<ActionResult> CreateStudentAsync(StudentRequestDTO dto)
        {
            await _studentService.CreateStudentAsync(dto);
            return Created();
        }

        [HttpGet]
        public async Task<ActionResult<List<StudentResponseDTO>>> GetAllStudentsAsync()
        {
            var students = await _studentService.GetAllStudentsAsync();
            if (students is null)
                return NotFound("Não há estudantes cadastrados!");

            return Ok(students);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudentAsync(Guid id, StudentUpdateDTO student)
        {
            try
            {
                await _studentService.UpdateStudentAsync(id, student);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudentAsync(Guid id)
        {
            var rowsAfected = await _studentService.DeleteStudentAsync(id);
            if (rowsAfected > 0)
                return NoContent();
            else
                return NotFound("Estudante não encontrado!");
        }
    }
}
