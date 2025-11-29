namespace DevLearning.Api.Models.Dtos.Course
{
    public class UpdateCourseDto
    {
        public string? Summary { get; set; }
        public bool? Active { get; set; }
        public bool? Free { get; set; }
        public bool? Featured { get; set; }
        public string? Tags { get; set; }
    }
}
