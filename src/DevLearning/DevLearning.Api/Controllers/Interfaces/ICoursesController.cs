using DevLearning.Api.Models.Dtos.Course;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.Api.Controllers.Interfaces
{
    public interface ICoursesController
    {
        Task<IActionResult> CreateCourseAsync(CreateCourseDto course);

        Task<ActionResult<List<CourseResponseDto>>> GetAllCoursesAsync();

        Task<ActionResult<CourseResponseDto?>> GetCourseByIdAsync(Guid id);

        Task<IActionResult> UpdateCourseAsync(Guid id, UpdateCourseDto update);
        
        Task<IActionResult> DeleteCourseAsync(Guid id);
    }
}
