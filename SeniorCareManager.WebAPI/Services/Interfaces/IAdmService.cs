using SeniorCareManager.WebAPI.Controllers.Dtos;

namespace SeniorCareManager.WebAPI.Services.Interfaces;

public interface IAdmService
{
    Task<string> LoginAdm(RequestAdmLoginDTO login);
}
