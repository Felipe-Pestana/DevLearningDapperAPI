using DevLearning.Api.Data;
using DevLearning.Api.Models.Dtos.CareerItem;
using DevLearningAPI.Models;
using DevLearningAPI.Models.Dtos.Career;
using DevLearningAPI.Repositories;
using DevLearningAPI.Repositories.Interfaces;
using DevLearningAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevLearningAPI.Services
{
    public class CareerService : ICareerService
    {

        private readonly CareerRepository _careerRepository;
        public CareerService(CareerRepository careerRepository)
        {
            _careerRepository = careerRepository;
        }

        public async Task<IEnumerable<Career>> GetAllAsync()
        {
            try
            {
                return await _careerRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Career> GetByIdAsync(Guid id)
        {
            try
            {
                var career = await _careerRepository.GetByIdAsync(id);
                if (career is null)
                    throw new KeyNotFoundException("Carreira não encontrada!");

                return career;
            }
            catch (KeyNotFoundException) { throw; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CreateAsync(CreateCareerDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Title))
                throw new ArgumentException("O título é obrigatório!");

            var career = new Career(dto.Title, dto.Summary, dto.Url, dto.Tags, dto.Featured);

            try
            {
                await _careerRepository.CreateAsync(career);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(Guid id, UpdateCareerDto dto)
        {
            try
            {
                var existing = await _careerRepository.GetByIdAsync(id);
                if (existing is null)
                    throw new KeyNotFoundException("Carreira não encontrada!");

            
                var updated = new Career(id, dto.Title, dto.Summary, dto.Url,
                                         existing.DurationInMinutes, dto.Active, dto.Featured, dto.Tags);

                await _careerRepository.UpdateAsync(updated);
            }
            catch (KeyNotFoundException) { throw; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var existing = await _careerRepository.GetByIdAsync(id);
                if (existing is null)
                    throw new KeyNotFoundException("Carreira não encontrada!");

                // Executa Soft Delete
                await _careerRepository.SoftDeleteAsync(id);
            }
            catch (KeyNotFoundException) { throw; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task AddItemAsync(Guid careerId, CreateCareerItemDTO dto)
        {
            if (dto.Order <= 0)
                throw new ArgumentException("A ordem deve ser maior que zero!");

            try
            {
                var existing = await _careerRepository.GetByIdAsync(careerId);
                if (existing is null)
                    throw new KeyNotFoundException("Carreira não encontrada!");

                var item = new CareerItem(careerId, dto.CourseId, dto.Title, dto.Description, dto.Order);

                await _careerRepository.AddItemAsync(item);
            }
            catch (KeyNotFoundException) { throw; }
            catch (ArgumentException) { throw; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}