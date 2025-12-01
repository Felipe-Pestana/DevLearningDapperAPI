namespace DevLearning.Api.Models.Dtos.Course
{
    public class CourseByAuthorDto
    {
        public string Tag { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public string Summary { get; init; } = string.Empty;
        public bool Active { get; init; }
        public Guid CategoryId { get; init; }
    }
}
