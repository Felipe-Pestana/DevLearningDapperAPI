using API.Database;
using API.Models;
using API.Models.DTOs.Category;
using API.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SqlConnection _connection;

        public CategoryRepository(DbConnectionFactory connectionFactory)
        {
            _connection = connectionFactory.GetConnection();
        }

        public async Task<List<CategoryResponseDTO>> GetAllCategoriesAsync()
        {
            var sqlString =
                @"SELECT Id, Title, Url, Summary, Order, Featured 
                FROM Category";

            return (await _connection.QueryAsync<CategoryResponseDTO>(sqlString)).ToList();
        }

        public async Task<CategoryResponseDTO> GetCategoryByIdAsync(Guid id)
        {
            var sqlString =
                @"SELECT Id, Title, Url, Summary, Order, Featured 
                FROM Category
                WHERE Id = @CategoryId";

            return await _connection.QueryFirstOrDefaultAsync<CategoryResponseDTO>(sqlString, new { CategoryId = id });
        }

        public async Task CreateCategoryAsync(Category category)
        {
            var sqlString =
                @"INSERT INTO Category (Title, Url, Summary, Order, Featured)
                VALUES (@Title, @Url, @Summary, @Order, @Featured)";

            await _connection.ExecuteAsync(sqlString, new { category.Title, category.Url, category.Summary, category.Order, category.Featured});
        }

        public async Task UpdateCategoryAsync(Category category, Guid id)
        {
            var sqlString =
                @"UPDATE Category SET Title = @Title, Summary = @Summary, Order = @Order, Featured = @Featured
                WHERE Id = @CategoryId";

            await _connection.ExecuteAsync(sqlString, new {category.Title, category.Summary, category.Order, category.Featured, CategoryId = id});
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var sqlString =
                @"DELETE FROM Category WHERE Id = @CategoryId";
            await _connection.ExecuteAsync(sqlString, new { CategoryId = id});
        }
    }
}
