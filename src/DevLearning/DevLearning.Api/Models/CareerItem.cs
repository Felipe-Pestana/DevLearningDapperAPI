public class CareerItem
{
    public Guid CareerId { get; private set; }
    public Guid CourseId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public byte Order { get; private set; }
    
    public CareerItem(Guid careerId, Guid courseId, string title, string description, byte order)
    {
        CareerId = careerId;
        CourseId = courseId;
        Title = title;
        Description = description;
        Order = order;
    }

}