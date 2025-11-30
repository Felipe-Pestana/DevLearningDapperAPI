namespace DevLearningAPI.Models.Dtos.Career
{

    public class CareerItemResponseDto
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }       
        public string Description { get; set; } 
        public byte Order { get; set; }
    }

    public class CareerResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Url { get; set; }
        public int DurationInMinutes { get; set; }
        public bool Active { get; set; }
        public bool Featured { get; set; }
        public string Tags { get; set; }

        public List<CareerItemResponseDto> Items { get; set; } = new List<CareerItemResponseDto>();
    }
}
    