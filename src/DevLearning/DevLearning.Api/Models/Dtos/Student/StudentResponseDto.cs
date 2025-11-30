namespace DevLearning.Api.Models.Dtos.Student
{
    public class StudentResponseDto
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string? Document { get; private set; }
        public string? Phone { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public DateTime CreateDate { get; private set; }
    }
}
