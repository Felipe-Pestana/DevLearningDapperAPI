using API.Models;
using API.Models.DTOs.Student;

namespace API.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        public Task CreateStudentAsync(Student student);
        public Task<List<StudentResponseDTO>> GetAllStudentsAsync();
        public Task<StudentUpdateDTO?> SearchStudentToUpdateAsync(Guid id);
        public Task UpdateStudentAsync(Guid id, StudentUpdateDTO student);
        public Task<int> DeleteStudentAsync(Guid id);
        public Task<bool> VerifyExistCourseAsync(Guid courseId);
        public Task<bool> VerifyExistStudentAsync(Guid studentId);
        public Task EnrollingStudentInCourseAsync(StudentCourse studentCourse);
    }
}
