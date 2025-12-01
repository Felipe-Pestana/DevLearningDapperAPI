namespace DevLearning.Api.Models.Dtos.Author
{
    public class CourseAuthorResponseDto
    {
        public Guid Id { get; init; }
        public string Tag { get; init; } = string.Empty;
        public string Course { get; init; } = string.Empty;
        public string Summary { get; init; } = string.Empty;
        public bool Active { get; init; }
        public Guid CategoryId { get; init; }
    }
}
