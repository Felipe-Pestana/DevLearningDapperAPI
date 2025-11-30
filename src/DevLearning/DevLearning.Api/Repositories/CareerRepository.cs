using Dapper;
using DevLearning.Api.Data;
using DevLearningAPI.Models;
using DevLearningAPI.Models.Dtos.Career;
using DevLearningAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Connections;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace DevLearningAPI.Repositories
{
    public class CareerRepository : ICareerRepository
    {
        private readonly SqlConnection _connection;
        public CareerRepository(ConnectionDB connection)
        {
            _connection = connection.GetConnection();
        }

        public async Task CreateAsync(Career career)
        {
            var sql = @"INSERT INTO [Career] (
                          [Id], [Title], [Summary], [Url], [DurationInMinutes], 
                          [Active], [Featured], [Tags]
                        )
                        VALUES (
                          @Id, @Title, @Summary, @Url, @DurationInMinutes, 
                          @Active, @Featured, @Tags
                        )";

            try
            {
                await _connection.ExecuteAsync(sql, new
                {
                    career.Id,
                    career.Title,
                    career.Summary,
                    career.Url,
                    career.DurationInMinutes,
                    career.Active,
                    career.Featured,
                    career.Tags
                });
            }
            catch (SqlException sqlex)
            {
                throw new Exception(sqlex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Career>> GetAllAsync()
        {
            var sql = @"SELECT [Id], [Title], [Summary], [Url], [DurationInMinutes], 
                               [Active], [Featured], [Tags]
                        FROM [Career] 
                        WHERE [Active] = 1";

            try
            {
                return await _connection.QueryAsync<Career>(sql);
            }
            catch (SqlException sqlex)
            {
                throw new Exception(sqlex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Career?> GetByIdAsync(Guid id)
        {
            var sql = @"SELECT 
                            c.[Id], c.[Title], c.[Summary], c.[Url], c.[DurationInMinutes], 
                            c.[Active], c.[Featured], c.[Tags],
                            ci.[CareerId], ci.[CourseId], ci.[Title], ci.[Description], ci.[Order]
                        FROM [Career] c
                        LEFT JOIN [CareerItem] ci ON c.[Id] = ci.[CareerId]
                        WHERE c.[Id] = @Id
                        ORDER BY ci.[Order]";

            try
            {
                Career? careerEntry = null;

                await _connection.QueryAsync<Career, CareerItem, Career>(
                    sql,
                    (career, item) =>
                    {
                        if (careerEntry == null) careerEntry = career;
                        if (item != null) careerEntry.AddItem(item);
                        return careerEntry;
                    },
                    new { Id = id },
                    splitOn: "CareerId");

                return careerEntry;
            }
            catch (SqlException sqlex)
            {
                throw new Exception(sqlex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(Career career)
        {
            var sql = @"UPDATE [Career] 
                        SET [Title] = @Title, [Summary] = @Summary, [Url] = @Url, 
                            [Active] = @Active, [Featured] = @Featured, [Tags] = @Tags
                        WHERE [Id] = @Id";

            try
            {
                await _connection.ExecuteAsync(sql, new
                {
                    career.Title,
                    career.Summary,
                    career.Url,
                    career.Active,
                    career.Featured,
                    career.Tags,
                    career.Id
                });
            }
            catch (SqlException sqlex)
            {
                throw new Exception(sqlex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            var sql = "UPDATE [Career] SET [Active] = 0 WHERE [Id] = @Id";

            try
            {
                await _connection.ExecuteAsync(sql, new { Id = id });
            }
            catch (SqlException sqlex)
            {
                throw new Exception(sqlex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddItemAsync(CareerItem item)
        {
            try
            {
                if (_connection.State != ConnectionState.Open) await _connection.OpenAsync();

                using var transaction = _connection.BeginTransaction();

                try
                {
                    var durationSql = "SELECT [DurationInMinutes] FROM [Course] WHERE [Id] = @Id";
                    var duration = await _connection.ExecuteScalarAsync<int>(durationSql, new { Id = item.CourseId }, transaction);

                    var insertSql = @"INSERT INTO [CareerItem] ([CareerId], [CourseId], [Title], [Description], [Order]) 
                                      VALUES (@CareerId, @CourseId, @Title, @Description, @Order)";

                    await _connection.ExecuteAsync(insertSql, new
                    {
                        item.CareerId,
                        item.CourseId,
                        item.Title,
                        item.Description,
                        item.Order
                    }, transaction);

                    var updateSql = @"UPDATE [Career] 
                                      SET [DurationInMinutes] = [DurationInMinutes] + @Duration 
                                      WHERE [Id] = @Id";

                    await _connection.ExecuteAsync(updateSql, new { Duration = duration, Id = item.CareerId }, transaction);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw; 
                }
            }
            catch (SqlException sqlex)
            {
                throw new Exception(sqlex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open) await _connection.CloseAsync();
            }
        }
    }
}