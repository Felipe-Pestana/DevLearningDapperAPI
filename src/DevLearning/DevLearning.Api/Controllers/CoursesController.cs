using DevLearning.Api.Controllers.Interfaces;
using DevLearning.Api.Models.Dtos.Course;
using DevLearning.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevLearning.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase, ICoursesController
    {
        private readonly CourseService _courseService;
        public CoursesController(CourseService courseService)
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
                return Problem(ex.Message, null, 400, "Invalid Input");
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

                return Ok(courses);
            }
            catch (Exception ex)
            {
                return Problem();
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
                return Problem(ex.Message, null, 404, "Course Not Found");
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message, null, 400, "Invalid Input");
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
                return Problem(ex.Message, null, 404, "Course Not Found");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
