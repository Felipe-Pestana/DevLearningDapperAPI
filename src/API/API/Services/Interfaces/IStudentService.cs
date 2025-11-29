using API.Models.DTOs.Student;

namespace API.Services.Interfaces
{
    public interface IStudentService
    {
        public Task CreateStudentAsync(StudentRequestDTO dto);
        public Task<List<StudentResponseDTO>> GetAllStudentsAsync();
        public Task UpdateStudentAsync(Guid id, StudentUpdateDTO student);
        public Task<int> DeleteStudentAsync(Guid id);
    }
}
