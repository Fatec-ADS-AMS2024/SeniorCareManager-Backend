using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;

namespace SeniorCareManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService service)
        {
            this._companyService = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var companies = await _companyService.GetAll();
            return Ok(companies);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var company = await _companyService.GetById(id);
            if (company == null) return NotFound("Empresa não encontrada!");
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CompanyDTO company)
        {
            try
            {
                await _companyService.Create(company);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar inserir uma nova empresa.");
            }
            return Ok(company);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CompanyDTO company)
        {
            try
            {
                await _companyService.Update(company, id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar atualizar a empresa.");
            }
            return Ok(company);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _companyService.Remove(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar excluir a empresa.");
            }
            return NoContent();
        }

    }
}
