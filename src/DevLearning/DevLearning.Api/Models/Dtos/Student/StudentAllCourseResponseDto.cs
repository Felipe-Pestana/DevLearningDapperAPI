using DevLearning.Api.Models.Dtos.Course;

namespace DevLearning.Api.Models.Dtos.Student
{
    public class StudentAllCourseResponseDto
    {
        public string Name { get; init; }
        public string Email { get; init; }
        public List<CoursePerStudentDto> Courses { get; set; } = new List<CoursePerStudentDto>();
    }
}
