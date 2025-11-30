namespace DevLearning.Api.Models.Dtos.Student
{
    public class StudentAllCourseResponseDto
    {
        public string Name { get; init; }
        public string Email { get; init; }
        public List<CourseResponseDto> Courses { get; set; } = new List<CourseResponseDto>();
    }
}
