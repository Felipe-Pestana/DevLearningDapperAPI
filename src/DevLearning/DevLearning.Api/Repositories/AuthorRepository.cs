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
                var sql = @"INSERT INTO Author FROM (Id, Name, Title, Image, Bio, Url, Email, Type) 
                            VALUES (@Name, @Title, @Image, Type)";

                await _connection.ExecuteAsync(sql, new { author.Id, author.Name, author.Title, author.Image, author.Bio, 
                                                    author.Url, author.Email, author.Type });                
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


        public async Task UpdateAuthorByIdAsync(Author author, Guid id)
        {
            try
            {
                var sql = @"UPDATE Author SET Name = @Name, Title = @Title, Image = Image, 
                            Bio = @Bio, Url = @Url, Email = @Email, Type = @Type WHERE Id = @Id";

                await _connection.ExecuteAsync(sql, new { author.Name, author.Title, author.Image,
                    author.Bio, author.Url, author.Email, author.Type, Id = id});
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

    }
}
