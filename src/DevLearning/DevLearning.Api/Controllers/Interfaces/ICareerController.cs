using DevLearning.Api.Models.Dtos.CareerItem;
using DevLearning.Api.Models;
using DevLearning.Api.Models.Dtos.Career;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.Api.Controllers.Interfaces
{
    public interface ICareerController
    {
        Task<ActionResult<List<Career>>> GetAllCareer();

        Task<ActionResult<Career>> GetCareerById(Guid id);
        Task<ActionResult> CreateCareer(CreateCareerDTO dto);
        Task<ActionResult> UpdateCareer(Guid id, UpdateCareerDto dto);
        Task<ActionResult> UpdateActiveCareer(Guid id);
        Task<ActionResult> AddItemCareer(Guid id, CreateCareerItemDTO dto);
        Task<ActionResult> RemoveItemCareer(Guid careerId, Guid courseId);


    }
}
