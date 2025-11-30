using DevLearning.Api.Models.Dtos.CareerItem;
using DevLearningAPI.Models;
using DevLearningAPI.Models.Dtos.Career;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.Api.Services.Interfaces
{
    public interface ICareerService
    {
        Task<IEnumerable<Career>> GetAllAsync();
        Task<Career> GetByIdAsync(Guid id);
        Task CreateAsync(CreateCareerDTO dto);
        Task UpdateAsync(Guid id, UpdateCareerDto dto);
        Task DeleteAsync(Guid id);
        Task AddItemAsync(Guid careerId, CreateCareerItemDTO dto);
    }
}