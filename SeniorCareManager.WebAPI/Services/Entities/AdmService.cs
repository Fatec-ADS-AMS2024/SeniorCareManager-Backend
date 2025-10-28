using Microsoft.IdentityModel.Tokens;
using SeniorCareManager.WebAPI.Authentication;
using SeniorCareManager.WebAPI.Controllers.Dtos;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Services.Entities;

public class AdmService : IAdmService
{
    private JwtService _jwt;

    public AdmService(JwtService jwt)
    {
        _jwt = jwt;
    }

    public async Task<string> LoginAdm(RequestAdmLoginDTO login)
    {
        if (login.Login != "adm" || login.Password != "adm@132")
            throw new ExceptionUnauthorized("Credenciais inválidas!");

        var token = _jwt.GenerateJwtToken([]);

        if (token == null || token.IsNullOrEmpty())
            throw new ExceptionNotFound("Erro ao tentar gerar o token! Tente com outras credenciais!");

        return await Task.FromResult(token);
    }
}
