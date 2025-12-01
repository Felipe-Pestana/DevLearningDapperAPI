using DevLearning.API.Models;
using DevLearning.API.Models.DTOs.Student;

namespace DevLearning.API.Services.Interfaces
{
    public interface IStudentService
    {
        public Task CreateStudent(StudentRequestDTO student);
        public Task DeleteStudent(string id);
        public Task UpdateStudent(StudentRequestUpdateDTO student, string id);
        public Task<List<StudentResponseDTO>> GetAllStudents();
        public Task<Student> GetStudentById(string id);
        public Task<Student> GetStudentByDocument(string document);
        public Task<Student> GetStudentByEmail(string email);
        public Task InsertStudentCourse(Guid studentId, Guid courseId, StudentRequestInsertCourseDTO studentCourse);
        public Task UpdateStudentCourse(Guid studentId, Guid courseId, StudentCourseRequestUpdateDTO studentCourse);
    }
}
