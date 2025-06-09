using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerService _manufacturerService;
        private readonly Response _response;

        public ManufacturerController(IManufacturerService service)
        {
            _manufacturerService = service;
            _response = new Response();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var manufacturers = await _manufacturerService.GetAll();
            _response.Code = ResponseEnum.Success;
            _response.Data = manufacturers;
            _response.Message = "Lista de fabricantes obtida com sucesso!";
            return Ok(_response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var manufacturer = await _manufacturerService.GetById(id);
                if (manufacturer is null)
                {
                    _response.Code = ResponseEnum.NotFound;
                    _response.Message = "Fabricante não encontrado.";
                    return NotFound(_response);
                }

                _response.Code = ResponseEnum.Success;
                _response.Message = $"Fabricante {manufacturer.CorporateName} obtido com sucesso!";
                _response.Data = manufacturer;
                return Ok(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao tentar obter fabricante.";
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Manufacturer manufacturer)
        {
            manufacturer.CpfCnpj = StringValidator.ExtractNumbers(manufacturer.CpfCnpj);
            manufacturer.Email = manufacturer.Email?.Trim();
            manufacturer.CorporateName = StringValidator.RemoveDiacritics(manufacturer.CorporateName);
            manufacturer.TradeName = StringValidator.RemoveDiacritics(manufacturer.TradeName);
            manufacturer.Phone = StringValidator.ExtractNumbers(manufacturer.Phone);

            if (string.IsNullOrWhiteSpace(StringValidator.RemoveDiacritics(manufacturer.CorporateName)))
                return BadRequest("O nome da empresa deve conter apenas letras e espaços.");

            if (string.IsNullOrWhiteSpace(manufacturer.Email) || !manufacturer.Email.Contains("@"))
                return BadRequest("O e-mail do fornecedor é obrigatório e deve ser válido.");

            if (string.IsNullOrWhiteSpace(StringValidator.RemoveDiacritics(manufacturer.TradeName)))
                return BadRequest("O nome fantasia deve conter apenas letras e espaços.");

            if (!CpfCnpjValidator.IsValidCNPJ(manufacturer.CpfCnpj) && !CpfCnpjValidator.IsValidCPF(manufacturer.CpfCnpj))
                return BadRequest("O CPF ou CNPJ fornecido é inválido.");

            if (!StringValidator.ExtractNumbers(manufacturer.Phone).Length.Equals(11))
                return BadRequest("O número de telefone no formato incorreto.");

            try
            {
                manufacturer.Id = 0;
                await _manufacturerService.Create(manufacturer);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Fabricante cadastrado com sucesso!";
                _response.Data = manufacturer;
                return Ok(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao cadastrar fabricante.";
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Manufacturer manufacturer)
        {
            manufacturer.CpfCnpj = StringValidator.ExtractNumbers(manufacturer.CpfCnpj);
            manufacturer.Email = manufacturer.Email?.Trim();
            manufacturer.CorporateName = StringValidator.RemoveDiacritics(manufacturer.CorporateName);
            manufacturer.TradeName = StringValidator.RemoveDiacritics(manufacturer.TradeName);
            manufacturer.Phone = StringValidator.ExtractNumbers(manufacturer.Phone);

            if (string.IsNullOrWhiteSpace(StringValidator.RemoveDiacritics(manufacturer.CorporateName)))
                return BadRequest("O nome da empresa deve conter apenas letras e espaços.");

            if (string.IsNullOrWhiteSpace(manufacturer.Email) || !manufacturer.Email.Contains("@"))
                return BadRequest("O e-mail do fornecedor é obrigatório e deve ser válido.");

            if (string.IsNullOrWhiteSpace(StringValidator.RemoveDiacritics(manufacturer.TradeName)))
                return BadRequest("O nome fantasia deve conter apenas letras e espaços.");

            if (!CpfCnpjValidator.IsValidCNPJ(manufacturer.CpfCnpj) && !CpfCnpjValidator.IsValidCPF(manufacturer.CpfCnpj))
                return BadRequest("O CPF ou CNPJ fornecido é inválido.");

            if (!StringValidator.ExtractNumbers(manufacturer.Phone).Length.Equals(11))
                return BadRequest("O número de telefone no formato incorreto.");

            try
            {
                await _manufacturerService.Update(manufacturer, id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Fabricante atualizado com sucesso!";
                _response.Data = manufacturer;
                return Ok(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao tentar atualizar fabricante.";
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var manufacturer = await _manufacturerService.GetById(id);
                if (manufacturer is null)
                {
                    _response.Code = ResponseEnum.NotFound;
                    _response.Message = "Fabricante não encontrado.";
                    return NotFound(_response);
                }

                await _manufacturerService.Remove(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Fabricante excluído com sucesso!";
                return Ok(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao tentar excluir fabricante.";
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }
    }
}