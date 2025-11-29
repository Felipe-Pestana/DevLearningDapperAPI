namespace API.Models.DTOs.Course
{
    public class CourseRequestDTO
    {
        public string Tag { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public string Summary { get; init; } = string.Empty;
        public string Url { get; init; } = string.Empty;
        public string Level { get; init; } = string.Empty;
        public int DurationInMinutes { get; init; } = 0;
        public bool Active { get; init; } = false;
        public bool Free { get; init; } = false;
        public bool Featured { get; init; } = false;
        public int AuthorId { get; init; } = 0;
        public int CategoryId { get; init; } = 0;
    }
}
