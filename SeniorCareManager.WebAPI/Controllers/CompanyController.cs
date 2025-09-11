using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;

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

        [HttpPost("{id}/upload-logo")]
        public async Task<IActionResult> UploadLogo(int id, IFormFile logo)
        {
            if (logo?.Length <= 0)
                return BadRequest(new Response
                {
                    Code = ResponseEnum.Invalid,
                    Message = "Nenhuma imagem foi enviada.",
                    Data = null
                });

            CompanyDTO company;

            try
            {
                company = await _companyService.GetById(id); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Code = ResponseEnum.Error,
                    Message = $"Erro ao buscar empresa: {ex.Message}",
                    Data = null
                });
            }

            if (company is null)
                return NotFound(new Response
                {
                    Code = ResponseEnum.NotFound,
                    Message = "Empresa não encontrada.",
                    Data = null
                });

            try
            {
                using var ms = new MemoryStream();
                await logo.CopyToAsync(ms);

                var Dto = new CompanyDTO
                {
                    Id = company.Id,
                    CompanyLogo = ms.ToArray(),
                };

                await _companyService.UpdateLogo(Dto); 

                return Ok(new Response
                {
                    Code = ResponseEnum.Success,
                    Message = "Logo da empresa atualizado com sucesso!",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Code = ResponseEnum.Error,
                    Message = $"Erro ao atualizar logo: {ex.Message}",
                    Data = null
                });
            }
        }


        [HttpGet("{id}/logo-base64")]
        public async Task<IActionResult> GetCompanyLogoBase64(int id)
        {
            var company = await _companyService.GetById(id);
            if (company == null || company.CompanyLogo == null)
            {
                return NotFound(new Response
                {
                    Code = ResponseEnum.NotFound,
                    Message = "Logo não encontrado.",
                    Data = null
                });
            }

            var base64 = Convert.ToBase64String(company.CompanyLogo);
            var mimeType = "image/png"; // ou o tipo real
            var dataUrl = $"data:{mimeType};base64,{base64}";

            return Ok(new Response
            {
                Code = ResponseEnum.Success,
                Message = "Logo obtido com sucesso.",
                Data = dataUrl
            });
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
