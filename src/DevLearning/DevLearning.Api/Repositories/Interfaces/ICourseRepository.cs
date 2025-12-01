using DevLearning.Api.Models;
using DevLearning.Api.Models.Dtos.Course;

namespace DevLearning.Api.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task CreateCourseAsync(Course course);

        Task<List<CourseResponseDto>> GetAllCoursesAsync();

        Task<CourseResponseDto?> GetCourseByIdAsync(Guid id);

        Task<CourseDataDto?> GetCourseToUpdateAsync(Guid id);

        Task<bool> GetCourseTitleAsync(string title);

        Task<bool> GetCourseUrlAsync(string url);

        Task UpdateCourseAsync(Guid id, Course course);

        Task DeleteCourseAsync(Guid id);

        Task<int> DeleteCourseByAuthorIdAsync(Guid id);

        Task<int> DeleteCourseByCategoryIdAsync(Guid id);
    }
}
