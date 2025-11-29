using DevLearning.Api.Repositories;

namespace DevLearning.Api.Services
{
    public class CourseService
    {
        private readonly CourseRepository _courseRepository;
        public CourseService(CourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

    }
}
