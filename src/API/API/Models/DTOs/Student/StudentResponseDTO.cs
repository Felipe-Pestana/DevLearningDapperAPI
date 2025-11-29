namespace API.Models.DTOs.Student
{
    public class StudentResponseDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
        public DateTime Birthdate { get; init; }
    }
}
