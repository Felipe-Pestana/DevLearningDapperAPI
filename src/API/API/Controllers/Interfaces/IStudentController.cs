using API.Models.DTOs.Student;
using API.Models.DTOs.StudentCourse;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Interfaces
{
    public interface IStudentController
    {
        public Task<ActionResult> CreateStudentAsync(StudentRequestDTO dto);
        public Task<ActionResult<List<StudentResponseDTO>>> GetAllStudentsAsync();
        public Task<ActionResult> UpdateStudentAsync(Guid id, StudentUpdateDTO student);
        public Task<ActionResult> DeleteStudentAsync(Guid id);
        public Task<ActionResult> EnrollingStudentInCourseAsync(Guid studentId, Guid courseId, StudentCourseRequestDTO dto);
    }
}
