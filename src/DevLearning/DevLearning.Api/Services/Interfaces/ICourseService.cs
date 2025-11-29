using DevLearning.Api.Models.Dtos.Course;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.Api.Services.Interfaces
{
    public interface ICourseService
    {
        public Task CreateCourseAsync(CreateCourseDto course);

        public Task<List<CourseResponseDto>> GetAllCoursesAsync();

        public Task<CourseResponseDto?> GetCourseByIdAsync(Guid id);

        public Task UpdateCourseAsync(Guid id, UpdateCourseDto update);
    }
}
