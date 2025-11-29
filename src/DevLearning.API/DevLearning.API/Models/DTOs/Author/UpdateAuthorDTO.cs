namespace DevLearning.API.Models.DTOs.Author
{
    public class UpdateAuthorDTO
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Bio { get; set; }
        public string Url { get; set; }
        public bool Type { get; set; }
    }
}
