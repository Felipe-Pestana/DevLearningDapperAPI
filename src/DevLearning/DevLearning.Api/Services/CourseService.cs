using DevLearning.Api.Models;
using DevLearning.Api.Models.Dtos.Course;
using DevLearning.Api.Repositories;
using DevLearning.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<CourseResponseDto?> GetCourseByIdAsync(Guid id)
        {
            try
            {
                return await _courseRepository.GetCourseByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateCourseAsync(Guid id, UpdateCourseDto update)
        {
            try
            {
                var oldCourse = await _courseRepository.GetCourseByIdAsync(id) ?? 
                    throw new Exception("Course not found!");

                var updatedCourse = new Course(
                    oldCourse.Tag, oldCourse.Title,
                    update.Summary ?? oldCourse.Summary,
                    oldCourse.Url, oldCourse.Level, oldCourse.DurationInMinutes,
                    DateTime.Now, DateTime.Now,
                    update.Active ?? oldCourse.Active,
                    update.Free ?? oldCourse.Free,
                    update.Featured ?? oldCourse.Featured,
                    oldCourse.AuthorId, oldCourse.CategoryId,
                    update.Tags ?? oldCourse.Tags
                    );

                await _courseRepository.UpdateCourseAsync(id, updatedCourse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
