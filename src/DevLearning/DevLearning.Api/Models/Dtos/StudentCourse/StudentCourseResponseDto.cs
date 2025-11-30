namespace DevLearning.Api.Models.Dtos.StudentCourse
{
    public class StudentCourseResponseDto
    {
        public Guid CourseId { get; private set; }
        public Guid StudentId { get; private set; }
        public int Progress { get; private set; }
        public bool Favorite { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime LastUpdateDate { get; private set; }
    }
}
