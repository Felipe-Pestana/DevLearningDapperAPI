namespace DevLearning.Api.Models.Dtos.Course
{
    public class CreateCourseDto
    {
        public string Tag { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public string Summary { get; init; } = string.Empty;
        public string Url { get; init; } = string.Empty;
        public string Level { get; init; } = string.Empty;
        public int DurationInMinutes { get; init; }
        public bool Free { get; init; }
        public bool Featured { get; init; }
        public Guid AuthorId { get; init; }
        public Guid CategoryId { get; init; }
        public string Tags { get; init; } = string.Empty;
    }
}
