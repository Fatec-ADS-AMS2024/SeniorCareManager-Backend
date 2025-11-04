using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Repositories
{
    public class ResidentAllergyRepository : GenericRepository<ResidentAllergy>, IResidentAllergyRepository
    {
        private readonly AppDbContext _context;
        public ResidentAllergyRepository(AppDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
