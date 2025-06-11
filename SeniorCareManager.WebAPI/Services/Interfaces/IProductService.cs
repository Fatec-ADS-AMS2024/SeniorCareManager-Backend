using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;

namespace SeniorCareManager.WebAPI.Services.Interfaces
{
    public interface IProductService : IGenericService<Product, ProductDTO>
    {
        new Task Remove(long id);
        new Task<ProductDTO> GetById(long id);
        new Task Update(ProductDTO entityDTO, long id);



    }
}
