using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;

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

    public override async Task<UserDTO> Create(UserDTO entityDTO)
    {
        var existing = await _userRepository.GetByEmail(entityDTO.Email);
        if (existing != null)
        {
            throw new ExceptionConflict("Já existe um usuário cadastrado com este e-mail.");
        }

        return await base.Create(entityDTO);
    }
    public override async Task Update(UserDTO entityDTO, int id)
    {
        if (entityDTO.Id != id)
            throw new ExceptionBadRequest("O id de Usuário dever ser o mesmo.");

        var existingEntity = await _userRepository.GetById(id);
        if (existingEntity == null)
        {
            throw new KeyNotFoundException($"Usuário com id {id} não encontrado.");
        }

        var emailOwner = await _userRepository.GetByEmail(entityDTO.Email);
        if (emailOwner != null && emailOwner.Id != id)
        {
            throw new ExceptionConflict("E-mail já está em uso por outro usuário.");
        }

        _mapper.Map(entityDTO, existingEntity);
        await _userRepository.Update(existingEntity);
    }
}
