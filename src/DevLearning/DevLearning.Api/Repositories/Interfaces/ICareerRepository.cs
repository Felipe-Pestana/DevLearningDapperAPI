using DevLearningAPI.Models;
using DevLearningAPI.Models.Dtos.Career;

namespace DevLearningAPI.Repositories.Interfaces
{
    public interface ICareerRepository
    {
        Task<IEnumerable<Career>> GetAllAsync();
        Task<Career?> GetByIdAsync(Guid id);
        Task CreateAsync(Career career);
        Task UpdateAsync(Career career);
        Task SoftDeleteAsync(Guid id); 
        Task AddItemAsync(CareerItem item); 
    }
}