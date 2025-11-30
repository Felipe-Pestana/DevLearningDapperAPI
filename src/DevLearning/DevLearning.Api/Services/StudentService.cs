using DevLearning.Api.Models;
using DevLearning.Api.Models.Dtos.Student;
using DevLearning.Api.Models.Dtos.StudentCourse;
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

            if (student.BirthDate >= DateTime.Now.Date)
            {
                throw new ArgumentException("Data de nascimento não pode ser atual ou futura!");
            }

            int number = 0;
            bool canConvert = int.TryParse(student.Phone, out number);
            if (canConvert == false)
            {
                throw new ArgumentException("Número de telefone inválido!");
            }

            var emailExist = await _repository.GetStudentByEmailAsync(student.Email);
            if (emailExist is not null) 
            {
                throw new ArgumentException("Email já cadastrado!");
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
            var students = await _repository.GetAllStudentsAsync();
            if (students is null)
                throw new ArgumentException("Lista Vazia!");

            try
            {
                return students;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task<StudentResponseDto?> GetStudentByIdAsync(Guid id)
        {
            var studentExist = await _repository.GetStudentByIdAsync(id);
            if (studentExist is null)
                throw new KeyNotFoundException("Estudante não encontrado!");

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
            var studentExist = await _repository.GetStudentByIdAsync(id);
            if (studentExist is null)
                throw new KeyNotFoundException("Estudante não encontrado!");

            int number = 0;
            bool canConvert = int.TryParse(student.Phone, out number);
            if (canConvert == false)
                throw new ArgumentException("Número de telefone inválido!");

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
            var studentExist = await _repository.GetStudentByIdAsync(id);
            if (studentExist is null)
                throw new KeyNotFoundException("Estudante não encontrado!");

            try
            {
                await _repository.DeleteStudentCourseAsync(id);
                await _repository.DeleteStudentAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task CreateStudentCourseAsync(Guid courseId, Guid studentId, CreateStudentCourseDto studentCourse)
        {

            var studentExist = await _repository.GetStudentByIdAsync(studentId);
            if (studentExist is null)
                throw new KeyNotFoundException("Estudante não encontrado!");

            try
            {
                var newStudentCourse = new StudentCourse
                (
                    courseId,
                    studentId,
                    studentCourse.Progress,
                    studentCourse.Favorite
                );

                await _repository.CreateStudentCourseAsync(newStudentCourse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task<StudentCourse?> GetStudentCourseAsync(Guid courseId, Guid studentId)
        {
            try
            {
               return await _repository.GetStudentCourseAsync(courseId, studentId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        //TODO: NÃO EXISTIR O GUID DO CURSO
        public async Task UpdateStudentCourseProgressAsync(Guid studentId, Guid courseId, UpdateStudentCourseDto student)
        {
            var studentExist = await _repository.GetStudentByIdAsync(studentId);
            if (studentExist is null)
                throw new KeyNotFoundException("Estudante não encontrado!");

            var studentCourseExist = await _repository.GetStudentCourseAsync(courseId, studentId);
            if (studentCourseExist is null)
                throw new KeyNotFoundException("Estudante não está cadastrado no curso informado!");

            try
            {
                await _repository.UpdateStudentCourseProgressAsync(courseId, studentId, student);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task<List<StudentResponseDto>> GetStudentAllCoursesAsync(Guid id)
        {
            var studentCourse = await _repository.GetStudentAllCoursesAsync(id);
            if (studentCourse is null)
                throw new KeyNotFoundException("Estudante não está matriculado em nenhum curso!");

            try
            {
                return studentCourse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

    }
}
