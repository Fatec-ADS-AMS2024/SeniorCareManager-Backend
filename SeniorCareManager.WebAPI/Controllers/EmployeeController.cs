using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeService.GetAll();
            return Response<IEnumerable<EmployeeDTO>>.Ok(employees, "Lista de funcionarios obtida com sucesso!");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _employeeService.GetById(id);
            return Response<EmployeeDTO>.Ok(employee, "Funcionário obtido com sucesso!");
        }

        [HttpPost]
        public async Task<IActionResult> Post(EmployeeDTO employeeDto)
        {
           
            Execute.Executar(employeeDto);
            employeeDto.Id = 0;
            await _employeeService.Create(employeeDto);

            return Response<EmployeeDTO>.Created(employeeDto, "Cargo Cadastrado com sucesso!");

            return Ok(employeeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EmployeeDTO employee)
        {
            try
            {
                await _employeeService.Update(employee, id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar atualizar os dados do funcionário: " + ex.Message);
            }
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _employeeService.Remove(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar remover o funcionário.");
            }
            return Ok("Funcionário removido com sucesso");
        }
    }
}

