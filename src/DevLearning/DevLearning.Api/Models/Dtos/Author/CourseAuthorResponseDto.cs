using DevLearning.Api.Models.Enum;

namespace DevLearning.Api.Models.Dtos.Author
{
    public class CourseAuthorResponseDto
    {
        public string Tag { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public string Summary { get; init; } = string.Empty;
        public bool Active { get; init; }
        public Guid CategoryId { get; init; }
    }
}
