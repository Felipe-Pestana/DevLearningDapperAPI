namespace DevLearning.Api.Models.Dtos.Course
{
    public class CourseResponseDto
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Tag { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Url { get; set; }
        public byte Level { get; set; }
        public int DurationInMinutes { get; set; }
        public bool Active { get; set; }
        public bool Free { get; set; }
        public bool Featured { get; set; }
        public string Tags { get; set; }
    }
}
