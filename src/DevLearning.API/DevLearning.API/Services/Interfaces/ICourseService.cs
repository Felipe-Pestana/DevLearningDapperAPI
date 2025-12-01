using DevLearning.API.Models;
using DevLearning.API.Models.DTOs.Course;

namespace DevLearning.API.Services.Interfaces
{
    public interface ICourseService
    {
        Task CreateCourseAsync(CourseRequestDTO course);

        Task<List<CourseResponseDTO>> GetAllCoursesAsync();

        Task<CourseResponseDTO> GetOneCourseByTitleAsync(string title);
        Task<CourseResponseDTO> GetOneCourseByIdAsync(string id);

        Task<CourseResponseDTO> DeleteCourseByIdAsync(string title);

        Task UpdateCourseAsync(string title, CourseUpdateDTO update);
    }
}
