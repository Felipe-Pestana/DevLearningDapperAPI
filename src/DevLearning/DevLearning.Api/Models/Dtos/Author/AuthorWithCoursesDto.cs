using DevLearning.Api.Models.Enum;

namespace DevLearning.Api.Models.Dtos.Author
{
    public class AuthorWithCoursesDto
    {
        public string Name { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public ETypeAuthor Type { get; init; }

        public List<CourseAuthorResponseDto> Courses { get; set; } = new();

    }
}
