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

        public Task<CourseDataDto?> GetCourseToUpdateAsync(Guid id);

        public Task<bool> GetCourseTitleAsync(string title);

        public Task<bool> GetCourseUrlAsync(string url);

        public Task UpdateCourseAsync(Guid id, Course course);

        public Task DeleteCourseAsync(Guid id);

        public Task<int> DeleteCourseByAuthorIdAsync(Guid id);

        public Task<int> DeleteCourseByCategoryIdAsync(Guid id);
    }
}
