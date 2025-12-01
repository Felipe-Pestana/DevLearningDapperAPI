namespace DevLearning.Api.Models.Dtos.Category
{
    public class UpdateCategoryDTO
    {
        public string? Summary { get; set; }
        public int? Order { get; set; }
        public string? Description { get; set; }
        public bool? Featured { get; set; }
    }
}
