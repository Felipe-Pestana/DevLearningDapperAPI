using DevLearning.Api.Models;
using DevLearning.Api.Models.Dtos.Student;
using DevLearning.Api.Models.Dtos.StudentCourse;

namespace DevLearning.Api.Services.Interfaces
{
    public interface IStudentService
    {

        Task CreateStudentAsync(CreateStudentDto student);
        Task<List<StudentResponseDto>> GetAllStudentsAsync();
        Task<StudentResponseDto?> GetStudentByIdAsync(Guid id);
        Task UpdateStudentAsync(Guid id, UpdateStudentDto student);
        Task DeleteStudentAsync(Guid id);
        Task CreateStudentCourseAsync(Guid courseId, Guid studentId, CreateStudentCourseDto studentCourse);
        Task<StudentCourse?> GetStudentCourseAsync(Guid courseId, Guid studentId);
        Task UpdateStudentCourseProgressAsync(Guid studentId, Guid courseId, UpdateStudentCourseDto student);
        Task<List<StudentResponseDto>> GetStudentAllCoursesAsync(Guid id);
    }
}
