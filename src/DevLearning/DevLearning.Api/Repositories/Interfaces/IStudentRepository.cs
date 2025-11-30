using DevLearning.Api.Models;
using DevLearning.Api.Models.Dtos.Student;
using DevLearning.Api.Models.Dtos.StudentCourse;

namespace DevLearning.Api.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task CreateStudentAsync(Student student);
        Task<List<StudentResponseDto>> GetAllStudentsAsync();
        Task<StudentResponseDto?> GetStudentByIdAsync(Guid id);
        Task<StudentResponseDto?> GetStudentByEmailAsync(string email);
        Task UpdateStudentAsync(Guid id, UpdateStudentDto student);
        Task DeleteStudentAsync(Guid id);
        Task CreateStudentCourseAsync(StudentCourse studentCourse);
        Task<StudentCourse?> GetStudentCourseAsync(Guid courseId, Guid studentId);
        Task UpdateStudentCourseProgressAsync(Guid studentId, Guid courseId, UpdateStudentCourseDto student);
        Task DeleteStudentCourseAsync(Guid studentId);
        Task<List<StudentResponseDto>> GetStudentAllCoursesAsync(Guid id);
    }
}
