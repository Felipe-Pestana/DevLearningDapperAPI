using DevLearning.Api.Models.Dtos.Course;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.Api.Controllers.Interfaces
{
    public interface ICoursesController
    {
        public Task<IActionResult> CreateCourseAsync(CreateCourseDto course);

        public Task<ActionResult<List<CourseResponseDto>>> GetAllCoursesAsync();
    }
}
