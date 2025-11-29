namespace DevLearning.Api.Models.Dtos
{
    public class CreateStudentDto
    {

        public string Name { get; init; }
        public string Email { get; init; }
        public string? Document { get; init; }
        public string? Phone { get; init; }
        public DateTime? BirthDate { get; init; }
        public DateTime CreateDate { get; init; }
    }
}
