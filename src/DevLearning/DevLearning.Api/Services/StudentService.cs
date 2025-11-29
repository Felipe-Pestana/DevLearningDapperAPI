using DevLearning.Api.Models;
using DevLearning.Api.Models.Dtos;
using DevLearning.Api.Repositories;

namespace DevLearning.Api.Services
{
    public class StudentService
    {
        public StudentRepository _repository;

        public StudentService(StudentRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateStudentAsync(CreateStudentDto student)
        {
            int number = 0;
            bool canConvert = int.TryParse(student.Phone, out number);

            if (student.BirthDate >= DateTime.Now.Date)
            {
                throw new ArgumentException("Data de nascimento não pode ser atual ou futura");
            }
            if (canConvert == false)
            {
                throw new ArgumentException("Número de telefone inválido!");
            }
            try
            {
                var newStudent = new Student
                (
                    student.Name,
                    student.Email,
                    student.Document,
                    student.Phone,
                    student.BirthDate
                );

                await _repository.CreateStudentAsync(newStudent);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task<List<StudentResponseDto>> GetAllStudentsAsync()
        {
            try
            {
                return await _repository.GetAllStudentsAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task<StudentResponseDto?> GetStudentByIdAsync(Guid id)
        {
            try
            {
                return await _repository.GetStudentByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task UpdateStudentAsync(Guid id, UpdateStudentDto student)
        {
            int number = 0;
            bool canConvert = int.TryParse(student.Phone, out number);
            if (canConvert == false)
            {
                throw new ArgumentException("Número de telefone inválido!");
            }

            try
            {
                await _repository.UpdateStudentAsync(id, student);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task DeleteStudentAsync(Guid id)
        {
            try
            {
                await _repository.DeleteStudentAsync(id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }
    }
}
