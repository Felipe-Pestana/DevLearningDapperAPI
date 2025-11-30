namespace DevLearning.Api.Models.Dtos.Course
{
    public class CoursePerStudentDto
    {
        public string Title { get; init; }
        public byte Level { get; init; }
        public int DurationInMinutes { get; init; }
        public bool Active { get; init; }
        public int Progress { get; init; }
    }
}
