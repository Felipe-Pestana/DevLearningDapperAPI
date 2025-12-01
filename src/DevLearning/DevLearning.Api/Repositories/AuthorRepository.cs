using Dapper;
using DevLearning.Api.Data;
using DevLearning.Api.Models;
using DevLearning.Api.Models.Dtos.Author;
using DevLearning.Api.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace DevLearning.Api.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly SqlConnection _connection;

        public AuthorRepository(ConnectionDB connection)
        {
            _connection = connection.GetConnection();
        }


        public async Task<List<AuthorResponseDto>> GetAllAuthorAsync()
        {
            try
            {
                var sql = "SELECT Name, Title, Image, Bio, Url, Email, Type FROM Author";
                return (await _connection.QueryAsync<AuthorResponseDto>(sql)).ToList();
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao consultar o banco de dados", ex);
            }
        }


        public async Task CreateAuthorAsync(Author author)
        {
            try
            {
                var sql = @"INSERT INTO Author (Id, Name, Title, Image, Bio, Url, Email, Type) 
                            VALUES (@Id, @Name, @Title, @Image, @Bio, @Url, @Email, @Type)";

                await _connection.ExecuteAsync(sql, new { author.Id, author.Name, author.Title, author.Image, author.Bio, 
                                                Url = author.Url == null ? (object)DBNull.Value : author.Url, author.Email, author.Type });                
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao consultar o banco de dados", ex);
            }

        }


        public async Task<AuthorResponseDto> GetAuthorByIdAsync(Guid id)
        {
            try
            {
                var sql = @"SELECT Name, Title, Image, Bio, Url, Email, Type 
                        FROM Author WHERE Id = @Id";

                return (await _connection.QueryFirstOrDefaultAsync<AuthorResponseDto>(sql, new { Id = id}))!;                
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao consultar o banco de dados", ex);
            }
        }


        public async Task<AuthorResponseDto> GetAuthorByEmailAsync(string email)
        {
            try
            {
                var sql = @"SELECT Name, Title, Image, Bio, Url, Email, Type 
                        FROM Author WHERE Email = @Email";

                return (await _connection.QueryFirstOrDefaultAsync<AuthorResponseDto>(sql, new { Email = email }))!;
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao consultar o banco de dados", ex);
            }
        }


        public async Task UpdateAuthorByIdAsync(Author author, Guid id)
        {
            try
            {
                var sql = @"UPDATE Author SET Title = @Title, Image = Image, Bio = @Bio, Url = @Url, Type = @Type WHERE Id = @Id";
                await _connection.ExecuteAsync(sql, new { author.Title, author.Image, author.Bio, author.Url, author.Type, Id = id});
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao consultar o banco de dados", ex);
            }
        }


        public async Task DeleteAuthorByIdAsync(Guid id)
        {
            try
            {
                var sql = "DELETE FROM Author WHERE Id = @Id";
                await _connection.ExecuteAsync(sql, new { Id = id });
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao consultar o banco de dados", ex);
            }
        }


        public async Task<AuthorWithCoursesDto> GetAuthorCoursesByIdAsync(Guid id)
        {
            try
            {
                var sql = @"SELECT a.Name, a.Title, a.Email, a.Type, 
                            c.Id, c.Tag, c.Title AS [Course], c.Summary, c.Active, c.CategoryId
                            FROM Author a
                            LEFT JOIN Course c
                            ON c.AuthorId = a.Id
                            WHERE a.Id = @Id";

                var authorDictionary = new Dictionary<string, AuthorWithCoursesDto>();

                var courses = await _connection.QueryAsync<AuthorWithCoursesDto, CourseAuthorResponseDto, AuthorWithCoursesDto>(
                        sql, (author, course) =>
                        {
                            // garante apenas 1 instância de Author
                            if (!authorDictionary.TryGetValue(author.Email, out var currentAuthor))
                            {
                                currentAuthor = author;
                                currentAuthor.Courses = new List<CourseAuthorResponseDto>();
                                authorDictionary.Add(currentAuthor.Email, currentAuthor);
                            }

                            // se houver course, adiciona
                            if (course != null && course.Id != Guid.Empty)
                                currentAuthor.Courses.Add(course);

                            return currentAuthor;
                        },
                        new { Id = id },
                        splitOn: "Id"
                    );

                return authorDictionary.Values.FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao consultar o banco de dados", ex);
            }
        }


    }
}
