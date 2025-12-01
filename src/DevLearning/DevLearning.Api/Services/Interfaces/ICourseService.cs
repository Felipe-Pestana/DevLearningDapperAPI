using DevLearning.Api.Models.Dtos.Course;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.Api.Services.Interfaces
{
    public interface ICourseService
    {
        Task CreateCourseAsync(CreateCourseDto course);

        Task<List<CourseResponseDto>> GetAllCoursesAsync();

        Task<CourseResponseDto?> GetCourseByIdAsync(Guid id);

        Task UpdateCourseAsync(Guid id, UpdateCourseDto update);

        Task DeleteCourseAsync(Guid id);

        Task<int> DeleteCourseByAuthorIdAsync(Guid id);

        Task<int> DeleteCourseByCategoryIdAsync(Guid id);
    }
}
