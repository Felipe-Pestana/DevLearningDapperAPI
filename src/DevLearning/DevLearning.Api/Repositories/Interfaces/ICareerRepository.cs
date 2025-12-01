using DevLearning.Api.Models;

namespace DevLearning.Api.Repositories.Interfaces
{
    public interface ICareerRepository
    {
        Task<IEnumerable<Career>> GetAllAsync();
        Task<Career?> GetByIdAsync(Guid id);
        Task CreateAsync(Career career);
        Task UpdateAsync(Career career);
        Task SoftDeleteAsync(Guid id); 
        Task AddItemAsync(CareerItem item);
        Task<bool> RemoveItemAsync(Guid careerId, Guid courseId);
        Task<List<Guid>> GetItemByCourseAsync(Guid courseId);
    }
}