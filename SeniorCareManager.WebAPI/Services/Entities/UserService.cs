using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;

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
    
    public async Task<UserDTO> GetByEmail(string email)
    {
        var user = await _userRepository.GetByEmail(email);
        return _mapper.Map<UserDTO>(user);
    }
}