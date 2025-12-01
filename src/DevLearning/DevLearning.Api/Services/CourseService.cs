using DevLearning.Api.Models;
using DevLearning.Api.Models.Dtos.Course;
using DevLearning.Api.Models.Enum;
using DevLearning.Api.Repositories.Interfaces;
using DevLearning.Api.Services.Interfaces;

namespace DevLearning.Api.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly Lazy<ICareerService> _careerService;
        private readonly Lazy<IStudentService> _studentService;
        private readonly Lazy<IAuthorService> _authorService;
        private readonly Lazy<ICategoryService> _categoryService;

        public CourseService(
            ICourseRepository courseRepository, Lazy<ICareerService> careerService, 
            Lazy<IStudentService> studentService, Lazy<IAuthorService> authorService, 
            Lazy<ICategoryService> categoryService
            )
        {
            _courseRepository = courseRepository;
            _careerService = careerService;
            _studentService = studentService;
            _authorService = authorService;
            _categoryService = categoryService;
        }

        public async Task CreateCourseAsync(CreateCourseDto course)
        {
            if (string.IsNullOrWhiteSpace(course.Tag))
                throw new ArgumentException("The field 'Tag' is mandatory!");
            if (string.IsNullOrWhiteSpace(course.Title))
                throw new ArgumentException("The field 'Title' is mandatory!");
            if (string.IsNullOrWhiteSpace(course.Summary))
                throw new ArgumentException("The field 'Summary' is mandatory!");
            if (string.IsNullOrWhiteSpace(course.Url))
                throw new ArgumentException("The field 'Url' is mandatory!");
            if (course.DurationInMinutes < 1)
                throw new ArgumentException("The field 'DurationInMinutes' must be over 0!");
            if (string.IsNullOrWhiteSpace(course.Tag))
                throw new ArgumentException("The field 'Tags' is mandatory!");

            if (await _courseRepository.GetCourseTitleAsync(course.Title))
                throw new ArgumentException("There is already a course with this title.");
            if (await _courseRepository.GetCourseUrlAsync(course.Url))
                throw new ArgumentException("There is already a course with this url.");

            var author = await _authorService.Value.GetAuthorByIdAsync(course.AuthorId);
            if (author.Type == ETypeAuthor.Inativo)
                throw new KeyNotFoundException("Author is inactive!");

            var category = await _categoryService.Value.GetCategoryByIdAsync(course.CategoryId);
            if (category is null)
                throw new KeyNotFoundException("Category not found!");

            if (!Enum.TryParse<ELevelCourse>(course.Level, true, out ELevelCourse level)) {
                throw new ArgumentException("The field 'Level' must be either 'Beginner', 'Intermediate' or 'Advanced'.");
            }

            var newCourse = new Course(
                course.Tag, course.Title, course.Summary, course.Url,
                level, course.DurationInMinutes, DateTime.Now,
                DateTime.Now, true, course.Free, course.Featured,
                course.AuthorId, course.CategoryId, course.Tags
                );

            await _courseRepository.CreateCourseAsync(newCourse);
        }

        public async Task<List<CourseResponseDto>> GetAllCoursesAsync()
        {
            try
            {
                return await _courseRepository.GetAllCoursesAsync();
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
            if ((update.Summary is not null) && string.IsNullOrWhiteSpace(update.Summary))
                throw new ArgumentException("The field 'Summary' cannot be changed to empty spaces!");

            if ((update.Tags is not null) && string.IsNullOrWhiteSpace(update.Tags))
                throw new ArgumentException("The field 'Tags' cannot be changed to empty spaces!");

            var oldCourse = await _courseRepository.GetCourseToUpdateAsync(id) ?? 
                throw new KeyNotFoundException($"No course was found with this ID.");

            var updatedCourse = new Course(
                oldCourse.Tag, oldCourse.Title,
                update.Summary ?? oldCourse.Summary,
                oldCourse.Url, oldCourse.Level, oldCourse.DurationInMinutes,
                oldCourse.CreateDate, DateTime.Now,
                update.Active ?? oldCourse.Active,
                update.Free ?? oldCourse.Free,
                update.Featured ?? oldCourse.Featured,
                oldCourse.AuthorId, oldCourse.CategoryId,
                update.Tags ?? oldCourse.Tags
                );

            await _courseRepository.UpdateCourseAsync(id, updatedCourse);
        }

        public async Task DeleteCourseAsync(Guid id)
        {
            var course = await _courseRepository.GetCourseByIdAsync(id) ??
                throw new KeyNotFoundException($"No course was found with this ID.");

            await _careerService.Value.RemoveItemByCourseAsync(id);

            await _studentService.Value.DeleteStudentCourseByCourseAsync(id);

            await _courseRepository.DeleteCourseAsync(id);
        }

        public async Task<int> DeleteCourseByAuthorIdAsync(Guid id)
        {
            return await _courseRepository.DeleteCourseByAuthorIdAsync(id);
        }

        public async Task<int> DeleteCourseByCategoryIdAsync(Guid id)
        {
            return await _courseRepository.DeleteCourseByCategoryIdAsync(id);
        }
    }
}
