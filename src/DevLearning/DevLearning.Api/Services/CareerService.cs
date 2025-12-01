using DevLearning.Api.Models;
using DevLearning.Api.Models.Dtos.Career;
using DevLearning.Api.Models.Dtos.CareerItem;
using DevLearning.Api.Repositories.Interfaces;
using DevLearning.Api.Services.Interfaces;

namespace DevLearning.Api.Services
{
    public class CareerService : ICareerService
    {

        private readonly ICareerRepository _careerRepository;
        public CareerService(ICareerRepository careerRepository)
        {
            _careerRepository = careerRepository;
        }

        public async Task<IEnumerable<Career>> GetAllAsync()
        {
            try
            {
                return await _careerRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Career> GetByIdAsync(Guid id)
        {
            try
            {
                var career = await _careerRepository.GetByIdAsync(id);
                if (career is null)
                    throw new KeyNotFoundException("Career not found!");

                return career;
            }
            catch (KeyNotFoundException) { throw; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CreateAsync(CreateCareerDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Title))
                throw new ArgumentException("This title is required!");

            var career = new Career(dto.Title, dto.Summary, dto.Url, dto.Tags, dto.Featured);

            try
            {
                await _careerRepository.CreateAsync(career);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(Guid id, UpdateCareerDto dto)
        {
            try
            {
                var existing = await _careerRepository.GetByIdAsync(id);
                if (existing is null)
                    throw new KeyNotFoundException("Career not found!");

            
                var updated = new Career(id, dto.Title, dto.Summary, dto.Url,
                                         existing.DurationInMinutes, dto.Active, dto.Featured, dto.Tags);

                await _careerRepository.UpdateAsync(updated);
            }
            catch (KeyNotFoundException) { throw; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateActiveAsync(Guid id)
        {
            try
            {
                var existing = await _careerRepository.GetByIdAsync(id);
                if (existing is null)
                    throw new KeyNotFoundException("Career not found!");

                await _careerRepository.SoftDeleteAsync(id);
            }
            catch (KeyNotFoundException) { throw; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task AddItemAsync(Guid careerId, CreateCareerItemDTO dto)
        {
            if (dto.Order <= 0)
                throw new ArgumentException("Order must be greater than 0!");

            try
            {
                var existing = await _careerRepository.GetByIdAsync(careerId);
                if (existing is null)
                    throw new KeyNotFoundException("Career not found!");

                var item = new CareerItem(careerId, dto.CourseId, dto.Title, dto.Description, dto.Order);

                await _careerRepository.AddItemAsync(item);
            }
            catch (KeyNotFoundException) { throw; }
            catch (ArgumentException) { throw; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task RemoveItemAsync(Guid careerId, Guid courseId)
        {
            if (careerId == Guid.Empty || courseId == Guid.Empty)
                throw new ArgumentException("Invalid IDs .");

            try
            {
                var existingCareer = await _careerRepository.GetByIdAsync(careerId);
                if (existingCareer is null)
                    throw new KeyNotFoundException("Career not found!");

                var removed = await _careerRepository.RemoveItemAsync(careerId, courseId);

                if (!removed)
                    throw new KeyNotFoundException("This course doesn't belong to this career.");
            }
            catch (KeyNotFoundException) { throw; } 
            catch (ArgumentException) { throw; }    
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task RemoveItemByCourseAsync(Guid courseId)
        {
            var careerIds = await _careerRepository.GetItemByCourseAsync(courseId);

            foreach(var careerId in careerIds)
            {
                await _careerRepository.RemoveItemAsync(careerId, courseId);
            }
        }
    }
}