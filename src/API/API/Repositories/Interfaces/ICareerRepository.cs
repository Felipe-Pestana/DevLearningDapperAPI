using API.Models;
using API.Models.DTOs.Career;

namespace API.Repositories.Interfaces
{
    public interface ICareerRepository
    {
        Task<IEnumerable<CareerResponseDTO>> GetAllCareerAsync();
        Task<CareerResponseDTO> GetCareerByIdAsync(Guid id);
        Task<Guid> CreateCareerAsync(CareerRequestDTO career);
        Task<bool> UpdateCareerAsync(Guid id, CareerRequestDTO career);

        //Task<int> SumDurationByCareerIdAsync(Guid careerId);

    }
}
