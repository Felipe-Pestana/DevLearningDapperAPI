using API.Models;
using API.Models.DTOs.Career;
using API.Repositories.Interfaces;
using API.Services.Interfaces;

namespace API.Services
{
    public class CareerService : ICareerService
    {
        private readonly ICareerRepository _repository;

        public CareerService(ICareerRepository repository)
        {
            _repository = repository;
        }
        public Task<IEnumerable<CareerResponseDTO>> GetAllCareerAsync() => _repository.GetAllCareerAsync();
        public Task<CareerResponseDTO?> GetCareerByIdAsync(Guid id) => _repository.GetCareerByIdAsync(id);
        public Task<Guid> CreateCareerAsync(CareerRequestDTO career) => _repository.CreateCareerAsync(career);
        public Task<bool> UpdateCareerAsync(Guid id, CareerRequestDTO career) => _repository.UpdateCareerAsync(id, career);
    }
}

