using DevLearning.Api.Models;
using DevLearning.Api.Models.Dtos.Course;

namespace DevLearning.Api.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        public Task CreateCourseAsync(Course course);

        public Task<List<CourseResponseDto>> GetAllCoursesAsync();
    }
}
