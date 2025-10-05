using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Repositories
{
    public class ResidentRepository : GenericRepository<Resident>, IResidentRepository
    {
        private readonly AppDbContext _context;

        public ResidentRepository(AppDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}