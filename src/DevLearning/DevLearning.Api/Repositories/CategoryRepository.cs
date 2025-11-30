using Dapper;
using DevLearning.Api.Data;
using DevLearning.Api.Models;
using DevLearning.Api.Models.Dtos;
using DevLearning.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;

namespace DevLearning.Api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SqlConnection _connection;

        public CategoryRepository(ConnectionDB connectionDB)
        {
            _connection = connectionDB.GetConnection();
        }

        public async Task CreateCategoryAsync(Category category)
        {
            var sql = @"INSERT INTO [Category]
                        ([Id], [Title], [Url], [Summary], [Order], [Description], [Featured])
                        VALUES (
                        @Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";

            try
            {
                await _connection.ExecuteAsync(
                    sql, new
                    {
                        category.Id,
                        category.Title,
                        category.Url,
                        category.Summary,
                        category.Order,
                        category.Description,
                        category.Featured
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

        public async Task<List<CategoryResponseDTO>> GetAllCategoriesAsync()
        {
            var sql = "SELECT title, url, summary, [order], description, featured FROM Category";

            try
            {
                return (await _connection.QueryAsync<CategoryResponseDTO>(sql)).ToList();
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

        public async Task<CategoryResponseDTO> GetCategoryByIdAsync(Guid id)
        {
            var sql = "SELECT title, url, summary, [order], description, featured FROM Category WHERE id = @CategoryId";

            try
            {
                return (await _connection.QueryFirstOrDefaultAsync<CategoryResponseDTO>(sql, new { CategoryId = id }));
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

        public async Task DeleteCategoryByIdAsync(Guid id)
        {
            var sql = "DELETE FROM [Category] WHERE Id = @Id";

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

        public async Task UpdateCategoryByIdAsync(Guid id, Category category)
        {
            var sql = @"UPDATE [Category] 
                        SET [Summary] = @Summary, [Order] = @Order,
                        [Description] = @Description, [Featured] = @Featured
                        WHERE Id = @Id";

            try
            {
                await _connection.ExecuteAsync(
                    sql, new
                    {
                        category.Summary,
                        category.Order,
                        category.Description,
                        category.Featured,
                        Id = id
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
    }
}
