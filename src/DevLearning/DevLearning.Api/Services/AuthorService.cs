using DevLearning.Api.Models;
using DevLearning.Api.Models.Dtos.Author;
using DevLearning.Api.Models.Enum;
using DevLearning.Api.Repositories.Interfaces;
using DevLearning.Api.Services.Interfaces;

namespace DevLearning.Api.Services
{
    public class AuthorService : IAuthorService
    {
        private IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }


        public async Task<List<AuthorResponseDto>> GetAllAuthorAsync()
        {
            return await _authorRepository.GetAllAuthorAsync();
        }


        public async Task<AuthorResponseDto> GetAuthorByIdAsync(Guid id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author is null) throw new KeyNotFoundException("Professor não encontrado.");

            return author;
        }


        public async Task<AuthorResponseDto> GetAuthorByEmailAsync(string email)
        {
            var author = await _authorRepository.GetAuthorByEmailAsync(email);
            if (author is null) throw new KeyNotFoundException("Professor não encontrado.");

            return author;
        }


        public async Task CreateAuthorAsync(CreateAuthorDto author)
        {
            var authorEmail = await _authorRepository.GetAuthorByEmailAsync(author.Email);
            if (authorEmail is not null) throw new ArgumentException("Error: Já existe professor com este email cadastrado.");

            if (author.Name.Length > 80) throw new ArgumentException("Error Nome: Atingiu o limite máximo de 80 caracteres.");
            if (author.Title.Length > 80) throw new ArgumentException("Error Título: Atingiu o limite máximo de 80 caracteres.");
            if (author.Image.Length > 1024) throw new ArgumentException("Error Imagem: Atingiu o limite máximo de 1024 caracteres.");
            if (author.Bio.Length > 2000) throw new ArgumentException("Error Bio: Atingiu o limite máximo de 2000 caracteres.");
            if (author.Email.Length > 160) throw new ArgumentException("Error Email: Atingiu o limite máximo de 160 caracteres.");
            if (!Enum.IsDefined(typeof(ETypeAuthor), author.Type)) throw new ArgumentException("Error: Tipo de professor inválido.");
            var strings = new List<string> { author.Name, author.Title, author.Image, author.Bio, author.Email };
            if (strings.Any(string.IsNullOrWhiteSpace)) throw new ArgumentException("Error: Houve campo obrigatório não informado.");

            var newAuthor = new Author(author.Name, author.Title, author.Image, author.Bio, author.Url, author.Email, author.Type);
            await _authorRepository.CreateAuthorAsync(newAuthor);
        }


        public async Task UpdateAuthorByIdAsync(UpdateAuthorDto authorUpdate, Guid id)
        {
            var oldAuthor = await _authorRepository.GetAuthorByIdAsync(id);
            if (oldAuthor is null) throw new KeyNotFoundException("Professor não encontrado.");

            if (authorUpdate.Title.Length > 80) throw new ArgumentException("Error Título: Atingiu o limite máximo de 80 caracteres.");
            if (authorUpdate.Image.Length > 1024) throw new ArgumentException("Error Imagem: Atingiu o limite máximo de 1024 caracteres.");
            if (authorUpdate.Bio.Length > 2000) throw new ArgumentException("Error Bio: Atingiu o limite máximo de 2000 caracteres.");
            if (!Enum.IsDefined(typeof(ETypeAuthor), authorUpdate.Type)) throw new ArgumentException("Error: Tipo de professor inválido.");

            var updatedAuthor = new Author(oldAuthor.Name, authorUpdate.Title ?? oldAuthor.Title,
            authorUpdate.Image ?? oldAuthor.Image, authorUpdate.Bio ?? oldAuthor.Bio,
            authorUpdate.Url ?? oldAuthor.Url, oldAuthor.Email, authorUpdate.Type ?? oldAuthor.Type
            );

            await _authorRepository.UpdateAuthorByIdAsync(updatedAuthor, id);
        }


        public async Task DeleteAuthorByIdAsync(Guid id)
        {
            var authorExist = await _authorRepository.GetAuthorByIdAsync(id);
            if (authorExist is null) throw new KeyNotFoundException("Professor não encontrado.");

            await _authorRepository.DeleteAuthorByIdAsync(id);
        }

    }
}
