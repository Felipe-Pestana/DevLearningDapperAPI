using DevLearning.Api.Data;
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


        public async Task CreateCareerAsync(CreateCareerDTO career)
        {
            var newCareer = new Career
            (
                career.Title,
                career.Summary,
                career.Url,
                career.DurationInMinutes,
                career.Featured,
                career.Tags
            );
            await _careerRepository.CreateCareerAsync(newCareer);
        }

        public async Task<List<CareerResponseDto>> GetAllCareersAsync()
        {
            return await _careerRepository.GetAllCareersAsync();
        }

        public async Task<CareerResponseDto> GetCareerByIdAsync(Guid careerId)
        {
            return await _careerRepository.GetCareerByIdAsync(careerId);
        }     
    }
}