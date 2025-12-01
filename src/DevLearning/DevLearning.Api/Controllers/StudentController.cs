using DevLearning.Api.Controllers.Interfaces;
using DevLearning.Api.Models.Dtos.Student;
using DevLearning.Api.Models.Dtos.StudentCourse;
using DevLearning.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase, IStudentController
    {
        public IStudentService _service;

        public StudentController(IStudentService service)
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
                return Ok(students);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentResponseDto>> GetStudentByIdAsync(Guid id)
        {
            try
            {
                var student = await _service.GetStudentByIdAsync(id);
                return Ok(student);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudentAsync(Guid id, UpdateStudentDto student)
        {
            try
            {
                await _service.UpdateStudentAsync(id, student);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudentAsync(Guid id)
        {
            try
            {
                await _service.DeleteStudentAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{studentId}/courses/{courseId}")]
        public async Task<ActionResult> CreateStudentCourseAsync(Guid courseId, Guid studentId, CreateStudentCourseDto studentCourse)
        {
            try
            {
                await _service.CreateStudentCourseAsync(courseId, studentId, studentCourse);
                return Created();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}/courses")]
        public async Task<ActionResult<StudentAllCourseResponseDto>> GetStudentAllCoursesAsync(Guid id)
        {
            try
            {
                var student = await _service.GetStudentAllCoursesAsync(id);
                return Ok(student);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{studentId}/courses/{courseId}/progress")]
        public async Task<ActionResult> UpdateStudentCourseProgressAsync(Guid studentId, Guid courseId, UpdateStudentCourseDto student)
        {
            try
            {
                await _service.UpdateStudentCourseProgressAsync(studentId, courseId, student);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
