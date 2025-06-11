using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Data.Repositories;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Services.Entities
{
    public class AllergyService : GenericService<Allergy, AllergyDTO>, IAllergyService
    {
        private readonly IAllergyRepository _allergyRepository;
        private readonly IMapper _mapper;

        public AllergyService(IAllergyRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _allergyRepository = repository;
            _mapper = mapper;
        }

        public override async Task<AllergyDTO> GetById(int id)
        {
            var allergy = await _allergyRepository.GetById(id);
            if (allergy is null)
                throw new KeyNotFoundException($"Allergy with id {id} not found.");
            return _mapper.Map<AllergyDTO>(allergy);
        }

        public override async Task<AllergyDTO> Create(AllergyDTO allergyDTO)
        {
            if (allergyDTO is null)
                throw new KeyNotFoundException("Id inválido");

            if (!allergyDTO.CheckName())
                throw new ArgumentException("Nome inválido.");

            if (await CheckDuplicates(allergyDTO.Name))
                throw new InvalidOperationException("Nome da alergia já existe.");

            base.Create(allergyDTO);
            return allergyDTO;
        }

        public override async Task<AllergyDTO> Update(AllergyDTO allergyDTO, int id)
        {
            var allergies = await _allergyRepository.Get();
            if (allergyDTO is null)
                throw new KeyNotFoundException("Id inválido");
            if (!allergyDTO.CheckName())
                throw new ArgumentException("Nome inválido.");
            if (await CheckDuplicates(allergyDTO.Name))
                throw new InvalidOperationException("Nome da alergia já existe.");
            await base.Update(allergyDTO, id);
            return allergyDTO;
        }

        public override async Task Remove(int id)
        {
            var allergies = await _allergyRepository.GetById(id);
            if (allergies is null)
                throw new KeyNotFoundException("Alergia com o id " + id + " informado não foi encontrada.");

            await base.Remove(id);
        }

        public async Task<bool> CheckDuplicates(string name)
        {
            var allergies = await _allergyRepository.Get();
            return allergies.Any(r => ValidatorString.CompareString(r.Name, name));
        }
    }
}

