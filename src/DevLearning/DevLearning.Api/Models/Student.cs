namespace DevLearning.Api.Models
{
    public class Student
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string? Document { get; private set; }
        public string? Phone { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public DateTime CreateDate { get; private set; }

        public Student(string name, string email, string? document, string? phone, DateTime? birthDate)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Document = document;
            Phone = phone;
            BirthDate = birthDate;
            CreateDate = DateTime.Now.Date;
        }
    }
}
