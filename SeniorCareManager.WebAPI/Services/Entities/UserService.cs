using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;

namespace SeniorCareManager.WebAPI.Services.Entities;

public class UserService : GenericService<User, UserDTO>, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UserService(IUserRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _userRepository = repository;
        _mapper = mapper;
    }

    private async Task<User?> GetByEmailInternal(string email)
    {
        return await _userRepository.GetByEmail(email);
    }

    public override async Task Create(UserDTO entityDTO)
    {
        Execute.Executar(entityDTO);

        var existing = await GetByEmailInternal(entityDTO.Email);
        if (existing != null)
        {
            throw new InvalidOperationException("Já existe um usuário cadastrado com este e-mail.");
        }
        await base.Create(entityDTO);
    }

    public override async Task Update(UserDTO entityDTO, int id)
    {
        Execute.Executar(entityDTO);

        var existingEntity = await _userRepository.GetById(id);
        if (existingEntity == null)
        {
            throw new KeyNotFoundException($"Entity with id {id} not found.");
        }

        var emailOwner = await GetByEmailInternal(entityDTO.Email);
        if (emailOwner != null && emailOwner.Id != id)
        {
            throw new InvalidOperationException("E-mail já está em uso por outro usuário.");
        }

        _mapper.Map(entityDTO, existingEntity);
        await _userRepository.Update(existingEntity);
    }
}