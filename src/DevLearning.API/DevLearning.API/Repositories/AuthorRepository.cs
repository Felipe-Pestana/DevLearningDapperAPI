using Dapper;
using DevLearning.API.DataBase;
using DevLearning.API.Models;
using DevLearning.API.Models.DTOs.Author;
using DevLearning.API.Models.Enums.Author;
using DevLearning.API.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace DevLearning.API.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        public readonly SqlConnection _connection;

        public AuthorRepository(ConnectionDB connection)
        {
            _connection = connection.GetConnection();
        }
        public async Task<List<AuthorResponseDTO>> GetAllAuthorsAsync()
        {
            var sql = "SELECT Name, Title, Image, Bio, Url, Email, Type FROM [Author]";

            var authors = (await _connection.QueryAsync<AuthorResponseDTO>(sql)).ToList();

            return authors;
        }
        public async Task<AuthorResponseDTO> GetAuthorByIdAsync(Guid id)
        {
            var sql = "SELECT Name, Title, Image, Bio, Url, Email, Type FROM [Author] WHERE Id = @Id";
            var author = await _connection.QueryFirstOrDefaultAsync<AuthorResponseDTO>(sql, new { Id = id });
            return author;
        }

        public async Task<AuthorResponseDTO> GetAuthorByEmail(string email)
        {
            var sql = "SELECT Name, Title, Image, Bio, Url, Email, Type FROM [Author] WHERE Email = @Email";
            var author = await _connection.QueryFirstOrDefaultAsync<AuthorResponseDTO>(sql, new { Email = email });
            return author;
        }
        public async Task CreateAuthorAsync(Author author)
        {
            var sql = @"INSERT INTO [Author] (Id, Name, Title, Image, Bio, Url, Email, Type) 
                        VALUES (@Id, @Name, @Title, @Image, @Bio, @Url, @Email, @Type) ";

            await _connection.ExecuteAsync(sql, new { author.Id, author.Name, author.Title, author.Image, author.Bio, author.Url, author.Email, author.Type });
        }
        public async Task DeleteAuthorAsync(Guid id)
        {
            //deletar os cursos do autor antes de deletar o autor
            var sqlDeleteCourse = "DELETE FROM [Course] WHERE AuthorId = @Id";
            await _connection.ExecuteAsync(sqlDeleteCourse, new { Id = id });

            var sql = "DELETE FROM [Author] WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task UpdatePatchAuthorAsync(UpdateAuthorParcialDTO author, Guid id)
        {
            var updates = new List<string>();

            string? name = null;
            string? title = null;
            string? image = null;
            string? bio = null;
            string? url = null;
            AuthorType? type = null;

            if (!string.IsNullOrWhiteSpace(author.Name))
            {
                name = author.Name;
                updates.Add("Name = @Name");

                // Atualiza URL automaticamente
                url = $"www.devlearning.com.br/author/{author.Name.ToLower().Replace(" ", "-")}";
                updates.Add("Url = @Url");
            }

            if (!string.IsNullOrWhiteSpace(author.Title))
            {
                title = author.Title;
                updates.Add("Title = @Title");
            }

            if (!string.IsNullOrWhiteSpace(author.Image))
            {
                image = author.Image;
                updates.Add("Image = @Image");
            }

            if (!string.IsNullOrWhiteSpace(author.Bio))
            {
                bio = author.Bio;
                updates.Add("Bio = @Bio");
            }
            if (author.Type.HasValue)
            {
                type = author.Type.Value;
                updates.Add("Type = @Type");
            }

            // Nada para atualizar
            if (!updates.Any())
                return;

            var sql = $"UPDATE Author SET {string.Join(", ", updates)} WHERE Id = @Id";

            await _connection.ExecuteAsync(sql, new
            {
                Name = name,
                Title = title,
                Image = image,
                Bio = bio,
                Url = url,
                Type = type,
                Id = id
            });
        }

        public async Task UpdatePutAuthorAsync(UpdateAuthorFullDTO author, Guid id)
        {
            string url = $"www.devlearning.com.br/author/{author.Name.ToLower().Replace(" ", "-")}";

            var sql = @"  UPDATE Author SET Name = @Name, Title = @Title, Image = @Image,
                    Bio = @Bio, Url = @Url WHERE Id = @Id";

            await _connection.ExecuteAsync(sql, new
            {
                Name = author.Name,
                Title = author.Title,
                Image = author.Image,
                Bio = author.Bio,
                Url = url,
                Id = id
                
            });
        }

        public async Task UpdateAuthorTypeAsync(Guid id, AuthorType newType)
        {
            var sql = "UPDATE Author SET Type = @Type WHERE Id = @Id";

            await _connection.ExecuteAsync(sql, new
            {
                Type = newType,
                Id = id
            });
        }


        //public async Task GetAuthorsCourses()  ---- FAZER DEPOIS A LÓGICA PARA PEGAR OS CURSOS DE CADA AUTOR
        //{
        //    var sql = @"SELECT a.Name AS AuthorName, c.Title AS CourseTitle
        //                FROM Author a
        //                JOIN Course c ON a.Id = c.AuthorId";
        //    var authorCourses = await _connection.QueryAsync(sql);
        //    
        //}
    }
}
