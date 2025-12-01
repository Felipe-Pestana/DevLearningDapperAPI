using DevLearning.API.Models;
using DevLearning.API.Models.DTOs.Student;

namespace DevLearning.API.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        public Task CreateStudent(Student student);
        public Task DeleteStudent(Guid id);
        public Task UpdateStudent(Student student, Guid id);
        public Task<List<StudentResponseDTO>> GetAllStudents();
        public Task<List<StudentWithCourseResponseDTO>> GetAllStudentsWithCourses();
        public Task<StudentWithCourseResponseDTO> GetStudentWithCoursesById(Guid studentId);
        public Task<Student> GetStudentByDocument(string document);
        public Task<Student> GetStudentByEmail(string email);
        public Task<Student> GetStudentById(Guid id);
        public Task InsertStudentCourse(Guid studentId, Guid courseId, StudentRequestInsertCourseDTO studentCourse);
        public Task UpdateStudentCourse(Guid studentId, Guid courseId, StudentCourseRequestUpdateDTO studentCourse);
    }
}
