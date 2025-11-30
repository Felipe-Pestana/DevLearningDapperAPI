using DevLearning.Api.Models.Enum;

namespace DevLearning.Api.Models.Dtos.Course
{
    public class CourseDataDto
    {
        public string Tag { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public string Summary { get; init; } = string.Empty;
        public string Url { get; init; } = string.Empty;
        public ELevelCourse Level { get; init; }
        public int DurationInMinutes { get; init; }
        public DateTime CreateDate { get; init; }
        public bool Active { get; init; }
        public bool Free { get; init; }
        public bool Featured { get; init; }
        public Guid AuthorId { get; init; }
        public Guid CategoryId { get; init; }
        public string Tags { get; init; } = string.Empty;
    }
}
