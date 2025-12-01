namespace DevLearning.Api.Models.Dtos.Course
{
    public class UpdateCourseDto
    {
        public string? Summary { get; init; }
        public bool? Active { get; init; }
        public bool? Free { get; init; }
        public bool? Featured { get; init; }
        public string? Tags { get; init; }
    }
}
