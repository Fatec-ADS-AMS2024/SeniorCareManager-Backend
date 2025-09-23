using AutoMapper;
using SeniorCareManager.WebAPI.Data; 
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;


namespace SeniorCareManager.WebAPI.Services.Entities
{
    public class AllergyService : GenericService<Allergy, AllergyDTO>, IAllergyService
    {
        private readonly IAllergyRepository _allergyRepository;
        private readonly IMapper _mapper;

        public AllergyService(IAllergyRepository repository, IMapper mapper, AppDbContext context) : base(repository, mapper)
        {
            _allergyRepository = repository;
            _mapper = mapper;
        }

        public override async Task<AllergyDTO> GetById(int id)
        {
            var errors = new List<FieldError>();
            var allergy = await _allergyRepository.GetById(id);

            if (allergy is null)
                throw new ExceptionBadRequest($"Alergia com o id {id} não foi encontrada.");

            return _mapper.Map<AllergyDTO>(allergy);
        }

        public override async Task Create(AllergyDTO allergyDTO)
        {
            var errors = new List<FieldError>();

            if (allergyDTO is null)
                throw new ExceptionBadRequest("Os dados da Alergia não podem ser nulos.");

            if (await _allergyRepository.ExistsByNameAsync(allergyDTO.Name))
                throw new ExceptionConflict("Uma alergia com este nome já existe.");

            await base.Create(allergyDTO);
        }

        public override async Task Update(AllergyDTO allergyDTO, int id)
        {
            var errors = new List<FieldError>();
            var existingEntity = await _allergyRepository.GetById(id);

            if (existingEntity == null)
            {
                throw new ExceptionBadRequest($"Alergia com id: {id} não encontrada para atualização.");
            }

            if (await _allergyRepository.ExistsByNameAsync(allergyDTO.Name, id))
            {
                throw new ExceptionConflict("O nome informado já pertence a outra alergia.");
            }

            _mapper.Map(allergyDTO, existingEntity);
            await _allergyRepository.Update(existingEntity);
        }

        public override async Task Remove(int id)
        {
            var allergy = await _allergyRepository.GetById(id);

            if (allergy is null)
                throw new ExceptionConflict($"Alergia com o id {id} não foi encontrada.");

            /* Validação de regra de negócio: verifica se a alergia está em uso. - Classe a ser implementada ResidentAllergy
            var isAllergyInUse = await _context.Set<ResidentAllergy>().AnyAsync(ra => ra.AllergyId == id);
            if (isAllergyInUse)
            {
                throw new InvalidOperationException("Esta alergia não pode ser removida pois está vinculada a um ou mais residentes.");
            }*/

            await base.Remove(id);
        }
    }
}