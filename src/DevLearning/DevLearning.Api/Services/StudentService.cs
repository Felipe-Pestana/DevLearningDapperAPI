using DevLearning.Api.Data;
using DevLearning.Api.Models;
using DevLearning.Api.Models.Dtos.Student;
using DevLearning.Api.Models.Dtos.StudentCourse;
using DevLearning.Api.Repositories;
using DevLearning.Api.Repositories.Interfaces;
using DevLearning.Api.Services.Interfaces;

namespace DevLearning.Api.Services
{
    public class StudentService : IStudentService
    {
        public StudentRepository _repository;
        private readonly CourseRepository _courseRepository;


        public StudentService(StudentRepository repository, CourseRepository courseRepository)
        {
            _repository = repository;
            _courseRepository = courseRepository;
        }

        public async Task CreateStudentAsync(CreateStudentDto student)
        {
            if(string.IsNullOrEmpty(student.Name))
                throw new ArgumentException("Name is mandatory!");

            if (string.IsNullOrEmpty(student.Document))
                throw new ArgumentException("Document is mandatory!");

            if (student.BirthDate >= DateTime.Now.Date)
                throw new ArgumentException("Date of birth cannot be current or future!");

            int number = 0;
            bool canConvert = int.TryParse(student.Phone, out number);
            if (canConvert == false)
                throw new ArgumentException("Invalid phone number!");

            var emailExist = await _repository.GetStudentByEmailAsync(student.Email);
            if (emailExist is not null) 
                throw new ArgumentException("Email already registered!");

            var documentExist = await _repository.GetStudentByDocumentAsync(student.Document);
            if (documentExist is not null)
                throw new ArgumentException("Document already registered!");

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
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<StudentResponseDto>> GetAllStudentsAsync()
        {
            var students = await _repository.GetAllStudentsAsync();
            if (students is null)
                throw new ArgumentException("Empty List!");

            try
            {
                return students;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<StudentResponseDto?> GetStudentByIdAsync(Guid id)
        {
            var studentExist = await _repository.GetStudentByIdAsync(id);
            if (studentExist is null)
                throw new KeyNotFoundException("Student not found!");

            try
            {
                return await _repository.GetStudentByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateStudentAsync(Guid id, UpdateStudentDto student)
        {
            var studentExist = await _repository.GetStudentByIdAsync(id);
            if (studentExist is null)
                throw new KeyNotFoundException("Student not found!");

            var documentExist = await _repository.GetStudentByDocumentAsync(student.Document);
            if (documentExist is not null)
                throw new ArgumentException("Document already registered!");

            int number = 0;
            bool canConvert = int.TryParse(student.Phone, out number);
            if (canConvert == false)
                throw new ArgumentException("Invalid phone number!");

            try
            {
                await _repository.UpdateStudentAsync(id, student);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteStudentAsync(Guid id)
        {
            var studentExist = await _repository.GetStudentByIdAsync(id);
            if (studentExist is null)
                throw new KeyNotFoundException("Student not found!");

            try
            {
                await _repository.DeleteStudentCourseAsync(id);
                await _repository.DeleteStudentAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CreateStudentCourseAsync(Guid courseId, Guid studentId, CreateStudentCourseDto studentCourse)
        {
            var courseExist = await _courseRepository.GetCourseByIdAsync(courseId);
            if (courseExist is null)
                throw new KeyNotFoundException("Course not found!");


            var studentExist = await _repository.GetStudentByIdAsync(studentId);
            if (studentExist is null)
                throw new KeyNotFoundException("Student not found!");

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
                throw new Exception(ex.Message);
            }
        }

        public async Task<StudentCourseResponseDto?> GetStudentCourseAsync(Guid courseId, Guid studentId)
        {
            try
            {
               return await _repository.GetStudentCourseAsync(courseId, studentId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateStudentCourseProgressAsync(Guid studentId, Guid courseId, UpdateStudentCourseDto student)
        {
            var courseExist = await _courseRepository.GetCourseByIdAsync(courseId);
            if (courseExist is null)
                throw new KeyNotFoundException("Course not found!");

            var studentExist = await _repository.GetStudentByIdAsync(studentId);
            if (studentExist is null)
                throw new KeyNotFoundException("Student not found!");

            var studentCourseExist = await _repository.GetStudentCourseAsync(courseId, studentId);
            if (studentCourseExist is null)
                throw new KeyNotFoundException("The student is not enrolled in the course indicated!");

            if(student.Progress < studentCourseExist.Progress)
                throw new ArgumentException("Progress cannot be any less than the current progress!");

            try
            {
                await _repository.UpdateStudentCourseProgressAsync( studentId, courseId, student);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<StudentAllCourseResponseDto>> GetStudentAllCoursesAsync(Guid id)
        {
            var studentExist = await _repository.GetStudentByIdAsync(id);
            if (studentExist is null)
                throw new KeyNotFoundException("Student not found!");

            var studentCourse = await _repository.GetStudentAllCoursesAsync(id);
            if (studentCourse is null)
                throw new KeyNotFoundException("The student is not enrolled in any course!");

            try
            {
                return studentCourse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
