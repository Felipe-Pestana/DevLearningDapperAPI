using DevLearning.Api.Models.Enum;

namespace DevLearning.Api.Models.Dtos.Author
{
    public class UpdateAuthorDto
    {
        public string? Title { get; init; }
        public string? Image { get; init; }
        public string? Bio { get; init; }
        public string? Url { get; init; }
        public ETypeAuthor? Type { get; init; }
    }
}
