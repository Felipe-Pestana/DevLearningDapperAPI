namespace DevLearning.Api.Models.Dtos.Course
{
    public class CourseDataDto
    {
        public string Tag { get; private set; }
        public string Title { get; private set; }
        public string Summary { get; private set; }
        public string Url { get; private set; }
        public byte Level { get; private set; }
        public int DurationInMinutes { get; private set; }
        public DateTime CreateDate { get; private set; }
        public bool Active { get; private set; }
        public bool Free { get; private set; }
        public bool Featured { get; private set; }
        public Guid AuthorId { get; private set; }
        public Guid CategoryId { get; private set; }
        public string Tags { get; private set; }
    }
}
