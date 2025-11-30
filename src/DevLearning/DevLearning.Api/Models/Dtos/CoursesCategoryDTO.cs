namespace DevLearning.Api.Models.Dtos
{
    public class CoursesCategoryDTO
    {
        public Guid IdCourse { get; private set; }
        public string TagCourse { get; private set; }
        public string TitleCourse { get; private set; }
        public string SummaryCourse { get; private set; }
        public Guid IdCategory { get; set; }
        public string TitleCategory { get; set; }
        public string DescriptionCategory { get; set; }

    }
}
