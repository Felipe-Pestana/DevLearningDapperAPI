using DevLearning.Api.Data;
using DevLearning.Api.Models.Dtos;

namespace DevLearning.Api.Repositories
{
    public class StudentRepository
    {
        public ConnectionDB _connectionDB;
        public StudentRepository(ConnectionDB connectionDB)
        {
            _connectionDB = connectionDB;
        }

        public async Task CreateStudentAsync() 
        {
            
        }

        public async Task<List<StudentResponseDto>> GetAllStudentsAsync() 
        {
            return null;    
        }

        public async Task UpdateStudentAsync(Guid id) 
        {
            
        }

        public async Task DeleteStudentAsync(Guid id)
        {

        }

    }
}
