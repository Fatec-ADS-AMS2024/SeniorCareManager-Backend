using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CompanyController : Controller
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
                _response.Code = ResponseEnum.Success;
                _response.Message = $"Empresa {company.CompanyName} obtida com sucesso!";
                _response.Data = company;
                return Ok(_response);
            }
            catch (KeyNotFoundException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = ex.Message;
                _response.Data = null;
                return NotFound(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao tentar obter empresa.";
                _response.Data = null;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CompanyDTO companyDto)
        {
            try
            {
                companyDto.Id = 0;
                await _companyService.Create(companyDto);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Empresa cadastrada com sucesso!";
                _response.Data = companyDto;
                return Ok(_response);
            }
            catch (ArgumentNullException ex)
            {
                _response.Code = ResponseEnum.Invalid;
                _response.Message = ex.Message;
                _response.Data = null;
                return BadRequest(_response);
            }
            catch (ArgumentException ex)
            {
                _response.Code = ResponseEnum.Invalid;
                _response.Message = ex.Message;
                _response.Data = null;
                return BadRequest(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.Code = ResponseEnum.Conflict;
                _response.Message = ex.Message;
                _response.Data = companyDto;
                return Conflict(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao cadastrar empresa.";
                _response.Data = companyDto;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CompanyDTO companyDto)
        {
            try
            {
                await _companyService.Update(companyDto, id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Empresa atualizada com sucesso!";
                _response.Data = companyDto;
                return Ok(_response);
            }
            catch (KeyNotFoundException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = ex.Message;
                _response.Data = null;
                return NotFound(_response);
            }
            catch (ArgumentNullException ex)
            {
                _response.Code = ResponseEnum.Invalid;
                _response.Message = ex.Message;
                _response.Data = companyDto;
                return BadRequest(_response);
            }
            catch (ArgumentException ex)
            {
                _response.Code = ResponseEnum.Invalid;
                _response.Message = ex.Message;
                _response.Data = companyDto;
                return BadRequest(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.Code = ResponseEnum.Conflict;
                _response.Message = ex.Message;
                _response.Data = companyDto;
                return Conflict(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao tentar atualizar empresa.";
                _response.Data = companyDto;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _companyService.Remove(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Empresa excluída com sucesso!";
                _response.Data = null; 
                return Ok(_response);
            }
            catch (KeyNotFoundException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = ex.Message;
                _response.Data = null;
                return NotFound(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao tentar excluir empresa.";
                _response.Data = null;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }
    }
}
