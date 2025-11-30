using DevLearning.API.Models;
using DevLearning.API.Models.DTOs.Student;

namespace DevLearning.API.Services.Interfaces
{
    public interface IStudentService
    {
        public Task CreateStudent(StudentRequestDTO student);
        public Task DeleteStudent(string id);
        public Task UpdateStudent(StudentRequestDTO student);
        public Task<List<Student>> GetAllStudents();
        public Task<Student> GetStudentById(string id);
        public Task<Student> GetStudentByDocument(string document);
    }
}
