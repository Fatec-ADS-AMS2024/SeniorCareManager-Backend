using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Repositories
{
    public class AllergyRepository : GenericRepository<Allergy>, IAllergyRepository
    {
        private readonly AppDbContext _context;
        public AllergyRepository(AppDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
