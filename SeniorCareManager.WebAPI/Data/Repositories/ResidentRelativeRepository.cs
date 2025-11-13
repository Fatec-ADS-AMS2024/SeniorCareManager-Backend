using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Repositories
{
    public class ResidentRelativeRepository : GenericRepository<ResidentRelative>, IResidentRelativeRepository
    {
        private readonly AppDbContext _context;
        public ResidentRelativeRepository(AppDbContext context) : base(context)
        {
            this._context = context;
        }
    }
    
}
