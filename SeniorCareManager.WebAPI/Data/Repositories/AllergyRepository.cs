using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> ExistsByNameAsync(string name)
        {
            // Compara ignorando maiúsculas/minúsculas diretamente no banco de dados.
            // Isso garante que "Glúten" e "glúten" sejam considerados duplicatas.
            return await _context.Allergies
                .AnyAsync(a => a.Name.ToLower() == name.ToLower());
        }

        
        // Verifica se OUTRA alergia com o mesmo nome já existe. Usado no UPDATE.
        public async Task<bool> ExistsByNameAsync(string name, int currentId)
        {
            return await _context.Allergies
                .AnyAsync(a => a.Name.ToLower() == name.ToLower() && a.Id != currentId);
        }
    }
}
