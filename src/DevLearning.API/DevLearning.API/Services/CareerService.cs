using Dapper;
using DevLearning.API.DataBase;
using DevLearning.API.Models;
using DevLearning.API.Models.DTOs.Carrer;
using DevLearning.API.Repositories;
using DevLearning.API.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace DevLearning.API.Services
{
    public class CareerService : ICareerService
    {
        public readonly CareerRepository careerRepository;
        private readonly ILogger<CareerService> logger;
        public CareerService(ILogger<CareerService> logger, CareerRepository careerRepository)
        {
            this.careerRepository = careerRepository;
            this.logger = logger;
        }

        public async Task CreateCareerAsync(CareerRequestDTO careerDTO)
        {
            try

            {
                var career = new Career(
                   careerDTO.Title,
                   careerDTO.Summary,
                   careerDTO.Title.ToLower().Replace(" ", "-"),
                   careerDTO.DurationInMinutes,
                   careerDTO.Tags
                );
                await careerRepository.CreateCareerAsync(career);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Erro interno ao criar carreira: {ex.Message}");
                throw;
            }
        }
        public async Task<List<CareerResponseDTO>> GetAllCareerAsync()
        {
            try { 
                
                var careers = await careerRepository.GetAllCareersAsync();
                return careers;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Erro interno ao listar todas as carreiras: {ex.Message}");
                throw;
            }
        }

        public async Task<CareerResponseDTO?> GetCareerByIdAsync(Guid careerId)
        {
            try
            {
                var career = await careerRepository.GetCareerByIdAsync(careerId);
                return career;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Erro interno ao buscar carreira por ID: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteCareerAsync(Guid careerId)
        {
            try
            {
                var result = await careerRepository.DeleteCareerAsync(careerId);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Erro interno ao deletar carreira: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateCareerAsync(Guid id, CareerUpdateDTO updateDTO)
        {
            try
            {

                var updates = new List<string>();
                var parameters = new DynamicParameters();
                parameters.Add("Id", id);

                if (!string.IsNullOrEmpty(updateDTO.Title))
                {
                    updates.Add("Title = @Title");
                    parameters.Add("Title", updateDTO.Title);
                    updates.Add("Url = @Url");
                    parameters.Add("Url", updateDTO.Title.ToLower().Replace(" ", "-"));
                }

                if (!string.IsNullOrEmpty(updateDTO.Summary))
                {
                    updates.Add("Summary = @Summary");
                    parameters.Add("Summary", updateDTO.Summary);
                }

                if (updateDTO.DurationInMinutes.HasValue)
                {
                    updates.Add("DurationInMinutes = @DurationInMinutes");
                    parameters.Add("DurationInMinutes", updateDTO.DurationInMinutes.Value);
                }

                if (updateDTO.Active.HasValue)
                {
                    updates.Add("Active = @Active");
                    parameters.Add("Active", updateDTO.Active.Value);
                }

                if (updateDTO.Featured.HasValue)
                {
                    updates.Add("Featured = @Featured");
                    parameters.Add("Featured", updateDTO.Featured.Value);
                }

                if (!string.IsNullOrEmpty(updateDTO.Tags))
                {
                    updates.Add("Tags = @Tags");
                    parameters.Add("Tags", updateDTO.Tags);
                }



                var result = await careerRepository.UpdateCareerAsync(id, updates, parameters);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Erro interno ao atualizar carreira: {ex.Message}");
                throw;
            }
        }


    }
}
