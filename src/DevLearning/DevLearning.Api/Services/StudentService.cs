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
            
        }
        //TRATAR - NÃO EXISTE O ESTUDANTE SOZINHO (CADASTRADO)
        //NÃO EXISTIR O GUID DO CURSO
        //NÃO EXISTIR RELAÇÃO ENTRE ESTUDANTE E CURSO

        public async Task UpdateStudentCourseProgressAsync(Guid id, UpdateStudentCourseDto student)
        {
            var studentExist = await _repository.GetStudentByIdAsync(id);
            if (studentExist is null)
                throw new KeyNotFoundException("Estudante não encontrado!");

            try
            {
                await _repository.UpdateStudentCourseProgressAsync(id, student);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

    }
}
