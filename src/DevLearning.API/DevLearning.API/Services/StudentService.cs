using DevLearning.API.Models;
using DevLearning.API.Models.DTOs.Student;
using DevLearning.API.Repositories;
using DevLearning.API.Services.Interfaces;

namespace DevLearning.API.Services
{
    public class StudentService : IStudentService
    {
        private StudentRepository _studentRepository;
        public StudentService(StudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task CreateStudent(StudentRequestDTO student)
        {
            try
            {
                var newStudent = new Student(student.Name, student.Email, student.Document, student.Phone, student.Birthdate);
                await _studentRepository.CreateStudent(newStudent);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteStudent(string id)
        {
            try
            {
                await _studentRepository.DeleteStudent(Guid.Parse(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<StudentResponseDTO>> GetAllStudents()
        {
            try
            {
                return await _studentRepository.GetAllStudents();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Student> GetStudentByEmail(string email)
        {
            try
            {
                return await _studentRepository.GetStudentByEmail(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Student> GetStudentByDocument(string document)
        {
            try
            {
                return await _studentRepository.GetStudentByDocument(document);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Student> GetStudentById(string id)
        {
           try
            {
                return await _studentRepository.GetStudentById(Guid.Parse(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateStudent(StudentRequestUpdateDTO student, string id)
        {
            try
            {
                var studentStorage = await _studentRepository.GetStudentById(Guid.Parse(id));
                if (studentStorage is null)
                    throw new Exception("Estudante não encontrado");
                if (await _studentRepository.GetStudentByDocument(student.Document) is not null)
                    throw new Exception("O documento informado já está cadastrado para outro estudante.");
                if (student.Email is not null && await _studentRepository.GetStudentByEmail(student.Email) is not null)
                    throw new Exception("O email informado já está cadastrado para outro estudante.");

                var newStudent = new Student(
                    student.Name is not null ? student.Name : studentStorage.Name,
                    student.Email is not null ? student.Email : studentStorage.Email,
                    student.Phone is not null ? student.Phone : studentStorage.Phone,
                    student.Document is not null ? student.Document : studentStorage.Document,
                    student.Birthdate is not null ? (DateTime)student.Birthdate : studentStorage.Birthdate
                    );
                await _studentRepository.UpdateStudent(newStudent, Guid.Parse(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}