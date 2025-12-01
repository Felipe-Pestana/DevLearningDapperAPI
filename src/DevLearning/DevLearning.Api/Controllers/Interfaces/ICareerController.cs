using DevLearning.Api.Models.Dtos.CareerItem;
using DevLearningAPI.Models.Dtos.Career;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.Api.Controllers.Interfaces
{
    public interface ICareerController
    {
        Task<ActionResult> GetAllCareer();

        Task<ActionResult> GetCareerById(Guid id);
        Task<ActionResult> CreateCareer(CreateCareerDTO dto);
        Task<ActionResult> UpdateCareer(Guid id, UpdateCareerDto dto);
        Task<ActionResult> DeleteCareer(Guid id);
        Task<ActionResult> AddItemCareer(Guid id, CreateCareerItemDTO dto);
        Task<ActionResult> RemoveItemCareer(Guid careerId, Guid courseId);


    }
}
