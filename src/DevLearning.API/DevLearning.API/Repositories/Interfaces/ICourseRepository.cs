using DevLearning.API.Models;
using DevLearning.API.Models.DTOs.Course;

namespace DevLearning.API.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task CreateCourseAsync(Course course);

        Task<List<CourseResponseDTO>> GetAllCoursesAsync();

        Task<CourseResponseDTO> GetOneCourseByIdAsync(Guid id);
        Task<CourseResponseDTO> GetOneCourseByTitleAsync(string title);

        Task<CourseResponseDTO> DeleteCourseByIdAsync(string title);

        Task UpdateCourseAsync(string title, bool active, bool free, bool featured, DateTime lastUpdate);
    }
}
