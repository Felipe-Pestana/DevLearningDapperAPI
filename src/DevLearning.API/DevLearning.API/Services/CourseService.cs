using DevLearning.API.Models;
using DevLearning.API.Models.DTOs.Course;
using DevLearning.API.Repositories;
using DevLearning.API.Services.Interfaces;

namespace DevLearning.API.Services
{
    public class CourseService : ICourseService
    {

        private CourseRepository _courseRepository;

        public CourseService(CourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task CreateCourseAsync(CourseRequestDTO course)
        {
            var newCourse = new Course(Guid.NewGuid(), course.Tag, course.Title, course.Summary, course.Url, course.Level, course.DurationInMinutes, DateTime.UtcNow, DateTime.UtcNow, true, false, false, course.AuthorId, course.CategoryId, course.Tags);
            await _courseRepository.CreateCourseAsync(newCourse);
        }

        public async Task<CourseResponseDTO> DeleteCourseByIdAsync(string title)
        {
            return await _courseRepository.DeleteCourseByIdAsync(title);
        }

        public async Task<List<CourseResponseDTO>> GetAllCoursesAsync()
        {
            return await _courseRepository.GetAllCoursesAsync();
        }

        public async Task<CourseResponseDTO> GetOneCourseByIdAsync(string title)
        {
            return await _courseRepository.GetOneCourseByIdAsync(title);
        }

        public async Task UpdateCourseAsync(string title, CourseUpdateDTO update)
        {
            await _courseRepository.UpdateCourseAsync(title, update.Active, update.Free, update.Featured, DateTime.UtcNow);
        }
    }
}
