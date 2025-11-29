using API.Controllers.Interfaces;
using API.Models.DTOs.Course;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase, ICourseController
    {
        private readonly ICourseService _courseService;
        private readonly ILogger<CourseController> _logger;

        public CourseController(ICourseService courseService, ILogger<CourseController> logger)
        {
            _courseService = courseService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<CourseResponseDTO>>> GetAllCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            if (courses == null || !courses.Any())
                return NoContent();
            return Ok(courses);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CourseResponseDTO>> GetCourseById(Guid id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound();
            return Ok(course);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCourse([FromBody] CourseRequestDTO dto)
        {
            await _courseService.CreateCourseAsync(dto);
            return Created(string.Empty, null);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateCourse(Guid id, [FromBody] CourseRequestDTO dto)
        {
            var ok = await _courseService.UpdateCourseAsync(id, dto);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteCourse(Guid id)
        {
            var ok = await _courseService.DeleteCourseAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
