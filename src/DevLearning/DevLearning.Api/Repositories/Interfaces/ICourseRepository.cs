using DevLearning.Api.Models;
using DevLearning.Api.Models.Dtos.Course;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace DevLearning.Api.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        public Task CreateCourseAsync(Course course);

        public Task<List<CourseResponseDto>> GetAllCoursesAsync();

        public Task<CourseResponseDto?> GetCourseByIdAsync(Guid id);

        public Task UpdateCourseAsync(Guid id, Course course);

        public Task DeleteCourseAsync(Guid id);
    }
}
