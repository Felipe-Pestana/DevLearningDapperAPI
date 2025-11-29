using DevLearningAPI.Models.Dtos.Career;
using Microsoft.AspNetCore.Mvc;

namespace DevLearningAPI.Services.Interfaces
{
    public interface ICareerService
    {
        Task CreateCareerAsync(CreateCareerDTO career);

        Task<List<CareerResponseDto>> GetAllCareersAsync();

        Task<CareerResponseDto> GetCareerByIdAsync(Guid careerId);
   
    }
}