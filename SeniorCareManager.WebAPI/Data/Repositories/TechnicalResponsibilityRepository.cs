using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Repositories
{
    public class TechnicalResponsibilityRepository : GenericRepository<TechnicalResponsibility>, ITechnicalResponsibilityRepository
    {
        private readonly AppDbContext _context;

        public TechnicalResponsibilityRepository(AppDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
