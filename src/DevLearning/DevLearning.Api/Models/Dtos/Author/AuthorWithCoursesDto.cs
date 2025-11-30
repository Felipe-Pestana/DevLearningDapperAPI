using DevLearning.Api.Models.Enum;

namespace DevLearning.Api.Models.Dtos.Author
{
    public class AuthorWithCoursesDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public string Bio { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public ETypeAuthor Type { get; init; }
    }
}
