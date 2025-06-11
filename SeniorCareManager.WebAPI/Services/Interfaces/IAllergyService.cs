using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Services.Interfaces
{
    public interface IAllergyService : IGenericService<Allergy, AllergyDTO>
    {
        public interface IAllergyService : IGenericService<Allergy, AllergyDTO>
        {
            Task<bool> IsFilledString(string name);
        }
    }
}
