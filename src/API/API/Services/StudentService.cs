using API.Models;
using API.Models.DTOs.Student;
using API.Models.DTOs.StudentCourse;
using API.Repositories;
using API.Services.Interfaces;

namespace API.Services
{
    public class StudentService : IStudentService
    {
        private StudentRepository _studentRepository;

        public StudentService(StudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task CreateStudentAsync(StudentRequestDTO dto)
        {
            var student = new Student(
                dto.Name,
                dto.Email,
                dto.Document,
                String.IsNullOrWhiteSpace(dto.Phone) ? null : dto.Phone,
                dto.Birthdate
            );

            await _studentRepository.CreateStudentAsync(student);
        }

        public async Task<int> DeleteStudentAsync(Guid id)
        {
            var rowsAffected = await _studentRepository.DeleteStudentAsync(id);
            return rowsAffected;
        }

        public async Task EnrollingStudentInCourseAsync(Guid studentId, Guid courseId, StudentCourseRequestDTO dto)
        {
            if (!await _studentRepository.VerifyExistStudentAsync(studentId))
                throw new ArgumentException("Esse estudante não existe!");

            if (!await _studentRepository.VerifyExistCourseAsync(courseId))
                throw new ArgumentException("Esse curso não existe!");

            var registration = new StudentCourse(
                courseId,
                studentId,
                dto.Progress,
                dto.Favorite
            );

            await _studentRepository.EnrollingStudentInCourseAsync(registration);
        }

        public async Task<List<StudentResponseDTO>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllStudentsAsync();
            if (students is null || students.Count() == 0)
                return null;

            return students;
        }

        public async Task UpdateStudentAsync(Guid id, StudentUpdateDTO student)
        {
            var studentFromDB = await _studentRepository.SearchStudentToUpdateAsync(id);
            if (studentFromDB is null)
                throw new ArgumentException("Estudante não encontrado para atualização");

            var studentToUpdate = new StudentUpdateDTO
            {
                Name = String.IsNullOrWhiteSpace(student.Name) ? studentFromDB.Name : student.Name,
                Email = String.IsNullOrWhiteSpace(student.Email) ? studentFromDB.Email : student.Email,
                Phone = String.IsNullOrWhiteSpace(student.Phone) ? studentFromDB.Phone : student.Phone
            };

            await _studentRepository.UpdateStudentAsync(id, studentToUpdate);
        }
    }
}
