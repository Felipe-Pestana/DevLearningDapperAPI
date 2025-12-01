using DevLearning.API.Models.DTOs.Course;
using DevLearning.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private CourseService _courseService;

        public CourseController(CourseService service)
        {
            _courseService = service;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<CourseResponseDTO>>> GetAllCoursesAsync()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpDelete("{title}")]
        public async Task<ActionResult> DeleteCourseByIdAsync(string title)
        {
            await _courseService.DeleteCourseByIdAsync(title);

            return Ok("Apagado!");
        }

        [HttpGet("{title}")]
        public async Task<ActionResult<CourseResponseDTO>> GetOneCourseByTitleAsync(string title)
        {
            var user = await _courseService.GetOneCourseByTitleAsync(title);
            return Ok(user);
        }

        [HttpPost("")]
        public async Task<ActionResult> CreateUserAsync(CourseRequestDTO course)
        {
            await _courseService.CreateCourseAsync(course);
            return Created();
        }

        [HttpPut("{title}")]
        public async Task<IActionResult> UpdateCourseAsync(string title, CourseUpdateDTO update)
        {
            await _courseService.UpdateCourseAsync(title, update);
            return Ok();
        }
    }
}
