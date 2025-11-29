namespace DevLearning.API.Models
{
    public class Student
    {
        public Student(string name, string email, string? document, string? phone, DateTime birthdate)
        {
            Name = name;
            Email = email;
            Document = document;
            Phone = phone;
            Birthdate = birthdate;
        }

        public Student()
        {
            
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string? Document { get; private set; }
        public string? Phone { get; private set; }
        public DateTime Birthdate { get; private set; }
        public DateTime CreateDate { get; private set; }

    }
}
