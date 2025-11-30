namespace API.Models
{
    public class CareerItem
    {
        public Guid CareerId { get; private set; }
        public Guid CourseId { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public int Order { get; private set; }
    }
}
