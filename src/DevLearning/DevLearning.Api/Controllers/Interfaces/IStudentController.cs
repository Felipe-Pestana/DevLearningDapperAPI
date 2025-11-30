using DevLearning.Api.Models.Dtos.Student;
using DevLearning.Api.Models.Dtos.StudentCourse;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.Api.Controllers.Interfaces
{
    public interface IStudentController
    {
        Task<ActionResult> CreateStudentAsync(CreateStudentDto student);
        Task<ActionResult<List<StudentResponseDto>>> GetAllStudentsAsync();
        Task<ActionResult<StudentResponseDto>> GetStudentByIdAsync(Guid id);
        Task<ActionResult> UpdateStudentAsync(Guid id, UpdateStudentDto student);
        Task<ActionResult> DeleteStudentAsync(Guid id);
        Task<ActionResult> CreateStudentCourseAsync(Guid courseId, Guid studentId, CreateStudentCourseDto studentCourse);
        Task<ActionResult<StudentAllCourseResponseDto>> GetStudentAllCoursesAsync(Guid id);

    }
}