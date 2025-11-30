using DevLearning.API.Models;
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

        [HttpGet("all")]
        public async Task<ActionResult<List<StudentResponseDTO>>> GetAllStudents()
        {
            try
            {
                var students = await _studentService.GetAllStudents();
                if (students is null && students.Count == 0)
                    return StatusCode(404, new { message = "Nenhum estudante encontrado" });
                else
                    return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetStudentById(string id)
        {
            try
            {
                var student = await _studentService.GetStudentById(id);
                if (student is null)
                    return StatusCode(404, new { message = "Estudante não encontrado" });
                else
                    return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("get-by-document")]
        public async Task<ActionResult> GetStudentByDocument([FromBody] StudentRequestDocumentDTO student)
        {
            try
            {
                var studentStorage = await _studentService.GetStudentByDocument(student.document);
                if (studentStorage is null)
                    return StatusCode(404, new { message = "Estudante não encontrado " });
                else
                    return Ok(studentStorage);
            } catch(Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("get-by-email")]
        public async Task<ActionResult> GetStudentByEmail([FromBody] StudentRequestEmailDTO student)
        {
            try
            {
                var studentStorage = await _studentService.GetStudentByEmail(student.email);
                if (studentStorage is null)
                    return StatusCode(404, new { message = "Estudante não encontrado " });
                else
                    return Ok(studentStorage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> CreateStudent([FromBody] StudentRequestUpdateDTO student, string id)
        {
            try
            {
                await _studentService.UpdateStudent(student, id);
                return StatusCode(204, new { message = "Cliente atualizado com sucesso" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task DeleteStudent(string id)
        {
            try
            {
                await _studentService.DeleteStudent(id);
                NoContent();
            }
            catch (Exception ex)
            {
                StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
