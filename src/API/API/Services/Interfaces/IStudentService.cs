using API.Models;
using API.Models.DTOs.Student;
using API.Models.DTOs.StudentCourse;

namespace API.Services.Interfaces
{
    public interface IStudentService
    {
        public Task CreateStudentAsync(StudentRequestDTO dto);
        public Task<List<StudentResponseDTO>> GetAllStudentsAsync();
        public Task UpdateStudentAsync(Guid id, StudentUpdateDTO student);
        public Task<int> DeleteStudentAsync(Guid id);
        public Task EnrollingStudentInCourseAsync(Guid studentId, Guid courseId, StudentCourseRequestDTO dto);
    }
}
