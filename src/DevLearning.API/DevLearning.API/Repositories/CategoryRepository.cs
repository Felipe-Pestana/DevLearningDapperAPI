using Dapper;
using DevLearning.API.DataBase;
using DevLearning.API.Models;
using DevLearning.API.Models.DTOs.Category;
using DevLearning.API.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace DevLearning.API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SqlConnection _connection;

        public CategoryRepository(ConnectionDB connection)
        {
            _connection = connection.GetConnection();
        }

        public async Task CreateCategoryAsync(Category category)
        {
            var sql = @"INSERT INTO Category (Id, Title, Url, Summary, [Order], Description, Featured) 
                      VALUES (@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";

            await _connection.ExecuteAsync(sql, new
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
        public async Task<List<CategoryResponseDTO>> GetAllCategoriesAsync()
        {
            var sql = @"SELECT Title, Url, Summary, [Order], Description, Featured
                        FROM Category
                        ORDER BY [Order]";

            var categories = await _connection.QueryAsync<CategoryResponseDTO>(sql);
            return categories.ToList();
        }

    }
}
