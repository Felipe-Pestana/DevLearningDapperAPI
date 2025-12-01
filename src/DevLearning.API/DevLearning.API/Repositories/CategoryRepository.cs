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

        private CourseRepository _courseRepository;
        public CategoryRepository(ConnectionDB connection, CourseRepository courseRepository)
        {
            _connection = connection.GetConnection();
            _courseRepository = courseRepository;
        }

        public async Task<bool> CategoryTitleExistsAsync(string title)
        {
            var sql = "SELECT COUNT(*) FROM Category WHERE Title = @Title";
            var count = await _connection.ExecuteScalarAsync<int>(sql, new { Title = title });
            return count > 0;
        }

        public async Task<bool> CategoryTitleExistsForOtherIdAsync(string title, Guid id)
        {
            var sql = "SELECT COUNT(*) FROM Category WHERE Title = @Title AND Id <> @Id";
            var count = await _connection.ExecuteScalarAsync<int>(sql, new { Title = title, Id = id });
            return count > 0;
        }

        public async Task ShiftOrdersAsync(int order)
        {
            var sql = @"UPDATE Category
                SET [Order] = [Order] + 1
                WHERE [Order] >= @Order";

            await _connection.ExecuteAsync(sql, new { Order = order });
        }

        public async Task ShiftOrdersForUpdateAsync(int oldOrder, int newOrder, Guid categoryId)
        {
            string sql;

            if (newOrder < oldOrder)
            {
                sql = @"UPDATE Category
                SET [Order] = [Order] + 1
                WHERE [Order] >= @NewOrder AND [Order] < @OldOrder
                AND Id <> @CategoryId";
            }
            else
            {
                sql = @"UPDATE Category
                SET [Order] = [Order] - 1
                WHERE [Order] > @OldOrder AND [Order] <= @NewOrder
                AND Id <> @CategoryId";
            }

            await _connection.ExecuteAsync(sql, new { NewOrder = newOrder, OldOrder = oldOrder, CategoryId = categoryId });
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
            var sql = @"SELECT Id, Title, Url, Summary, [Order], Description, Featured
                        FROM Category
                        ORDER BY [Order]";

            var categories = await _connection.QueryAsync<CategoryResponseDTO>(sql);
            return categories.ToList();
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            var sql = @"SELECT Id, Title, Url, Summary, [Order], Description, Featured
                        FROM Category
                        WHERE Id = @Id";

            var category = await _connection.QueryFirstOrDefaultAsync<Category>(sql, new { Id = id });
            return category;
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var sql = @"UPDATE Category
                        SET Title = @Title,
                        Url = @Url,
                        Summary = @Summary,
                        [Order] = @Order,
                        Description = @Description,
                        Featured = @Featured
                        WHERE Id = @Id";

            await _connection.ExecuteAsync(sql, new
            {
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured,
                category.Id
            });
        }
        public async Task DeleteCategoryAsync(Guid id)
        {
            //var deleteCoursesSql = "DELETE FROM Course WHERE CategoryId = @Id";
            //await _connection.ExecuteAsync(deleteCoursesSql, new { Id = id });

            var deleteCategorySql = "DELETE FROM Category WHERE Id = @Id";
            await _connection.ExecuteAsync(deleteCategorySql, new { Id = id });
        }
    }
}
