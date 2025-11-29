using DevLearning.Api.Models.Dtos.Course;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.Api.Controllers.Interfaces
{
    public interface ICoursesController
    {
        public Task<IActionResult> CreateCourseAsync(CreateCourseDto course);

        public Task<ActionResult<List<CourseResponseDto>>> GetAllCoursesAsync();

        public Task<ActionResult<CourseResponseDto?>> GetCourseByIdAsync(Guid id);

        public Task<IActionResult> UpdateCourseAsync(Guid id, UpdateCourseDto update);
    }
}
