
using Dapper;
using DevLearning.API.DataBase;
using DevLearning.API.Models;
using DevLearning.API.Models.DTOs.Carrer;
using DevLearning.API.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace DevLearning.API.Repositories
{
    public class CareerRepository : ICareerRepository
    {
        private readonly SqlConnection connection;
        private readonly ILogger<CareerRepository> logger;
        public CareerRepository(ConnectionDB connectionDB, ILogger<CareerRepository> logger)
        {
            this.connection = connectionDB.GetConnection();
            this.logger = logger;
        }


        public async Task CreateCareerAsync(Career career)
        {
            var sql = @"INSERT INTO Career (Id, Title, Summary, url, DurationInMinutes, Active, Featured, Tags)
                        VALUES (@Id, @Title, @Summary, @url, @DurationInMinutes, @Active, @Featured, @Tags)";
            var parameters = new
            {
                career.Id,
                career.Title,
                career.Summary,
                career.url,
                career.DurationInMinutes,
                career.Active,
                career.Featured,
                career.Tags
            };
            await connection.ExecuteAsync(sql, parameters);
        }

        public async Task<List<CareerResponseDTO>> GetAllCareersAsync()
        {
            try
            {
                var sql = @"SELECT 
                            Id, Title, Summary, url, DurationInMinutes, Active, Featured, Tags
                            FROM Career";
                var careers = (await connection.QueryAsync<CareerResponseDTO>(sql)).ToList();
                return careers;

            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Erro ao buscar todas as carreiras");
                throw;
            }
           
        }

        public async Task<CareerResponseDTO?> GetCareerByIdAsync(Guid Id)
        {
            try
            {
                var sql = @"SELECT 
                       Id, Title, Summary, url, DurationInMinutes, Active, Featured, Tags
                      FROM Career
                      WHERE Id = @Id";
                var career = await connection.QuerySingleOrDefaultAsync<CareerResponseDTO>(sql, new { Id = Id });
                return career;
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, $"Erro ao buscar carreira com Id {Id}");
                throw;
            }
               
        }

        public async Task <bool> UpdateCareerAsync(Guid id, List<string> updates, DynamicParameters parameters)
        {
            try
            {


                var sql = $"UPDATE Career SET {string.Join(", ", updates)} WHERE Id = @Id";
                var rows = await connection.ExecuteAsync(sql, parameters);

                return rows > 0 ? true : false;
            }
            catch (SqlException ex)
            {
                logger.LogError($"Erro ao atualizar usuário com Id {id}: {ex.Message}");
                throw;
            }
            
        }

        public async Task<bool> DeleteCareerAsync(Guid Id)
        {
            try
            {
                var sql = @"DELETE FROM Career WHERE Id = @Id";
                var rows = await connection.ExecuteAsync(sql, new { Id = Id });
                return rows > 0 ? true : false;
            }
            catch (SqlException ex)
            {
                logger.LogError($"Erro ao deletar carreira com Id {Id}: {ex.Message}");
                throw;
            }

        }

    }
}
