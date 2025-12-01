using DevLearning.Api.Controllers.Interfaces;
using DevLearning.Api.Models.Dtos.Course;
using DevLearning.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase, ICoursesController
    {
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourseAsync(CreateCourseDto course)
        {
            try
            {
                await _courseService.CreateCourseAsync(course);

                return Created();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<CourseResponseDto>>> GetAllCoursesAsync()
        {
            try
            {
                var courses = await _courseService.GetAllCoursesAsync();
                if (courses.Count < 1)
                    return Ok("No courses registered!");

                return Ok(courses);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseResponseDto?>> GetCourseByIdAsync(Guid id)
        {
            try
            {
                var course = await _courseService.GetCourseByIdAsync(id);
                if (course is null)
                    return NotFound("Course not found!");

                return Ok(course);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourseAsync(Guid id, UpdateCourseDto update)
        {
            try
            {
                await _courseService.UpdateCourseAsync(id, update);

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
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseAsync(Guid id)
        {
            try
            {
                await _courseService.DeleteCourseAsync(id);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
