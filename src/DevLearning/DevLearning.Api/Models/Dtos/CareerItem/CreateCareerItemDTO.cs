namespace DevLearning.Api.Models.Dtos.CareerItem
{
    public class CreateCareerItemDTO
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte Order { get; set; }
    }
}
