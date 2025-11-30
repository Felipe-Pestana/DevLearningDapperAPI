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
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteStudent(string id)
        {
            try
            {
                await _studentRepository.DeleteStudent(Guid.Parse(id));
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Student>> GetAllStudents()
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

        public async Task<Student> GetStudentByDocument(string document)
        {
           try
            {
                return await _studentRepository.GetStudentByDocument(document);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Student> GetStudentById(string id)
        {
           try
            {
                return await _studentRepository.GetStudentById(Guid.Parse(id));
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);    
            }
        }

        public Task UpdateStudent(StudentRequestUpdateDTO student)
        {
            throw new NotImplementedException();
        }
    }
}
