using DevLearning.Api.Models;
using DevLearning.Api.Models.Dtos.Author;
using DevLearning.Api.Models.Enum;
using DevLearning.Api.Repositories.Interfaces;
using DevLearning.Api.Services.Interfaces;

namespace DevLearning.Api.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ICourseService _courseService;

        public AuthorService(IAuthorRepository authorRepository, ICourseService courseService)
        {
            _authorRepository = authorRepository;
            _courseService = courseService;
        }


        public async Task<List<AuthorResponseDto>> GetAllAuthorAsync()
        {
            return await _authorRepository.GetAllAuthorAsync();
        }


        public async Task<AuthorResponseDto> GetAuthorByIdAsync(Guid id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author is null) throw new KeyNotFoundException("Professor not found.");

            return author;
        }


        public async Task<AuthorResponseDto> GetAuthorByEmailAsync(string email)
        {
            var author = await _authorRepository.GetAuthorByEmailAsync(email);
            if (author is null) throw new KeyNotFoundException("Professor not found.");

            return author;
        }


        public async Task CreateAuthorAsync(CreateAuthorDto author)
        {
            var authorEmail = await _authorRepository.GetAuthorByEmailAsync(author.Email);
            if (authorEmail is not null) throw new ArgumentException("Error: There is already a teacher registered with this email address.");

            if (author.Name.Length > 80) throw new ArgumentException("Error Name: The maximum limit of 80 characters has been reached.");
            if (author.Title.Length > 80) throw new ArgumentException("Error Title: The maximum limit of 80 characters has been reached.");
            if (author.Image.Length > 1024) throw new ArgumentException("Error Image: The maximum limit of 1024 characters has been reached.");
            if (author.Bio.Length > 2000) throw new ArgumentException("Error Bio: The maximum limit of 2000 characters has been reached.");
            if (author.Email.Length > 160) throw new ArgumentException("Error Email: The maximum limit of 160 characters has been reached.");
            if (!Enum.IsDefined(typeof(ETypeAuthor), author.Type)) throw new ArgumentException("Error: Invalid teacher type.");
            var strings = new List<string> { author.Name, author.Title, author.Image, author.Bio, author.Email };
            if (strings.Any(string.IsNullOrWhiteSpace)) throw new ArgumentException("Error: A required field was not filled in.");

            var newAuthor = new Author(author.Name, author.Title, author.Image, author.Bio, author.Url, author.Email, author.Type);
            await _authorRepository.CreateAuthorAsync(newAuthor);
        }


        public async Task UpdateAuthorByIdAsync(UpdateAuthorDto authorUpdate, Guid id)
        {
            var oldAuthor = await _authorRepository.GetAuthorByIdAsync(id);
            if (oldAuthor is null) throw new KeyNotFoundException("Professor not found.");

            var updatedAuthor = new Author(oldAuthor.Name, authorUpdate.Title ?? oldAuthor.Title,
            authorUpdate.Image ?? oldAuthor.Image, authorUpdate.Bio ?? oldAuthor.Bio,
            authorUpdate.Url ?? oldAuthor.Url, oldAuthor.Email, authorUpdate.Type ?? oldAuthor.Type
            );

            await _authorRepository.UpdateAuthorByIdAsync(updatedAuthor, id);
        }


        public async Task DeleteAuthorByIdAsync(Guid id)
        {
            var authorExist = await _authorRepository.GetAuthorByIdAsync(id);
            if (authorExist is null) throw new KeyNotFoundException("Professor not found.");

            await _courseService.DeleteCourseByAuthorIdAsync(id);
            await _authorRepository.DeleteAuthorByIdAsync(id);
        }


        public async Task<AuthorWithCoursesDto> GetAuthorCoursesAsync(Guid id)
        {
            var author = await _authorRepository.GetAuthorCoursesByIdAsync(id);
            if (author is null) throw new KeyNotFoundException("Professor not found.");

            return new AuthorWithCoursesDto
            {
                Name = author.Name,
                Title = author.Title,
                Email = author.Email,
                Type = author.Type,
                Courses = author.Courses.Select(c => new CourseAuthorResponseDto
                {
                    Id = c.Id,
                    Tag = c.Tag,
                    Course = c.Course,
                    Summary = c.Summary,
                    Active = c.Active,
                    CategoryId = c.CategoryId

                }).ToList()
            };
        }

    }
}
