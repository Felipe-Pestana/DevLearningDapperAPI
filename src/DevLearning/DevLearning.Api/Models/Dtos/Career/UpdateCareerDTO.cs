namespace DevLearningAPI.Models.Dtos.Career
{
    public class UpdateCareerDto
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }
        public bool Featured { get; set; }
        public string Tags { get; set; }
    }
}