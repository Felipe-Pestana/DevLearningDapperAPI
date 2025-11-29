using DevLearning.Api.Models;
using DevLearning.Api.Models.Dtos.Course;
using DevLearning.Api.Repositories;
using DevLearning.Api.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace DevLearning.Api.Services
{
    public class CourseService : ICourseService
    {
        private readonly CourseRepository _courseRepository;
        public CourseService(CourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task CreateCourseAsync(CreateCourseDto course)
        {
            try
            {
                var newCourse = new Course(
                    course.Tag, course.Title, course.Summary, course.Url,
                    course.Level, course.DurationInMinutes, DateTime.Now,
                    DateTime.Now, true, course.Free, course.Featured,
                    course.AuthorId, course.CategoryId, course.Tags
                    );

                await _courseRepository.CreateCourseAsync(newCourse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CourseResponseDto>> GetAllCoursesAsync()
        {
            try
            {
                return await _courseRepository.GetAllCoursesAsync();
            }
            catch (SqlException sqlex)
            {
                throw new Exception(sqlex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
