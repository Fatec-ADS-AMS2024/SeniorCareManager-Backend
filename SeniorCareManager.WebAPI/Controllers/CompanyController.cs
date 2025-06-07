using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly Response _response;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
            _response = new Response();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var companies = await _companyService.GetAll();
            _response.Code = ResponseEnum.Success;
            _response.Data = companies;
            _response.Message = "Lista de empresas obtida com sucesso!";
            return Ok(_response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var company = await _companyService.GetById(id);
                if (company is null)
                {
                    _response.Code = ResponseEnum.NotFound;
                    _response.Message = "Empresa não encontrada.";
                    return NotFound(_response);
                }

                _response.Code = ResponseEnum.Success;
                _response.Message = $"Empresa {company.CompanyName} obtida com sucesso!";
                _response.Data = company;
                return Ok(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao tentar obter empresa.";
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CompanyDTO company)
        {
            if (company is null || string.IsNullOrWhiteSpace(company.CompanyName))
            {
                _response.Code = ResponseEnum.Invalid;
                _response.Message = "Empresa inválida.";
                return BadRequest(_response);
            }

            try
            {
                company.Id = 0;
                await _companyService.Create(company);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Empresa cadastrada com sucesso!";
                _response.Data = company;
                return Ok(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao cadastrar empresa.";
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CompanyDTO company)
        {
            company.CNPJ = StringValidator.ExtractNumbers(company.CNPJ);
            company.Email = company.Email?.Trim();
            company.TradeName = StringValidator.RemoveDiacritics(company.TradeName);
            company.CompanyName = company.CompanyName?.Trim();
            company.PostalCode = StringValidator.ExtractNumbers(company.PostalCode);
            company.Number = StringValidator.ExtractNumbers(company.Number);

            if (string.IsNullOrWhiteSpace(company.CompanyName))
                return BadRequest("A razão social do fornecedor é obrigatória.");

            if (string.IsNullOrWhiteSpace(company.Email) || !company.Email.Contains("@"))
                return BadRequest("O e-mail do fornecedor é obrigatório e deve ser válido.");

            if (!StringValidator.ContainsOnlyLettersAndSpaces(company.TradeName))
                return BadRequest("O nome fantasia deve conter apenas letras e espaços.");

            if (!CpfCnpjValidator.IsValidCNPJ(company.CNPJ))
                return BadRequest("O CNPJ informado é inválido.");

            if (!StringValidator.ExtractNumbers(company.PostalCode).Length.Equals(8))
                return BadRequest("O Código postal informado deve conter 8 dígitos numéricos.");

            if (!StringValidator.ExtractNumbers(company.Number).Length.Equals(4))
                return BadRequest("O Número informado deve conter apenas dígitos numéricos.");

            try
            {
                await _companyService.Update(id, company);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Empresa atualizada com sucesso!";
                _response.Data = company;
                return Ok(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao tentar atualizar empresa.";
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var company = await _companyService.GetById(id);
                if (company is null)
                {
                    _response.Code = ResponseEnum.NotFound;
                    _response.Message = "Empresa não encontrada.";
                    return NotFound(_response);
                }

                await _companyService.Remove(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Empresa excluída com sucesso!";
                return Ok(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao tentar excluir empresa.";
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }
    }
}
