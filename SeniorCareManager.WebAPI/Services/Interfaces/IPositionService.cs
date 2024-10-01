using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Services.Interfaces
{
    public interface IPositionService: IGenericService<Position>
    {
        Task<IEnumerable<Position>> GetAll();
        Task<Position> GetById(int id);
        Task Create(Position entity);
        Task Update(Position entity, int id);
        Task Remove(int id);
    }
}
