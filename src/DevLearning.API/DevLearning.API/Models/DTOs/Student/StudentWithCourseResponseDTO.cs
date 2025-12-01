using DevLearning.API.Models.DTOs.Course;

namespace DevLearning.API.Models.DTOs.Student
{
    public class StudentWithCourseResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Document { get; set; }
        public string? Phone { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime CreateDate { get; set; }
        public List<CourseStudentDTO> Courses { get; set; }
    }
}
