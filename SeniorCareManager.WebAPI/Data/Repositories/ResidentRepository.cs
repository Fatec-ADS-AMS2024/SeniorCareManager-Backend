using SeniorCareManager.WebAPI.Data.Interfaces;

namespace SeniorCareManager.WebAPI.Data.Repositories;

public class ResidentRepository : GenericRepository<Resident>, IResidentRepository
{
    private readonly AppDbContext _context;
    public ResidentRepository(AppDbContext context) : base(context)
    {
        this._context = context;
    }
}
