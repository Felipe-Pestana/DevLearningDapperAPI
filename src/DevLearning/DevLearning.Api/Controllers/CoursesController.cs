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
    }
}
