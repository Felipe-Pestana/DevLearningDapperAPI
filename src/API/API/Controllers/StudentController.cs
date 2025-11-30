using API.Controllers.Interfaces;
using API.Models.DTOs.Student;
using API.Models.DTOs.StudentCourse;
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
        public async Task<ActionResult<List<StudentGetAllResponseDTO>>> GetAllStudentsAsync()
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

        [HttpPost("{studentId}/Course/{courseId}")]
        public async Task<ActionResult> EnrollingStudentInCourseAsync
        (
            Guid studentId, Guid courseId, StudentCourseRequestDTO dto
        )
        {
            try
            {
                await _studentService.EnrollingStudentInCourseAsync(studentId, courseId, dto);
                return Created();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{studentId}/Course/{courseId}/Progress")]
        public async Task<ActionResult> UpdateProgressStudentCourseAsync(Guid studentId, Guid courseId, StudentUpdateProgressDTO updateProgressDTO)
        {
            try
            {
                await _studentService.UpdateProgressStudentCourseAsync(studentId, courseId, updateProgressDTO);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.Contains("não existe!"))
                    return NotFound(ex.Message);
                else
                    return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}/Courses")]
        public async Task<ActionResult<StudentWithCoursesResponseDTO>> GetStudentCoursesAsync(Guid id)
        {
            try
            {
                var studentWithCourses = await _studentService.GetStudentCoursesAsync(id);
                if (studentWithCourses is null)
                    return NotFound("Estudante não encontrado");

                return Ok(studentWithCourses);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentGetByIdResponseDTO>> GetStudentByIdAsync(Guid id)
        {
            try
            {
                var student = await _studentService.GetStudentByIdAsync(id);
                if (student is null)
                    return NotFound("Estudante não encontrado!");

                return Ok(student);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
