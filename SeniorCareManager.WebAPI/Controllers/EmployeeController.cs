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
            return Response<EmployeeDTO>.Ok(employee, "Funcion�rio obtido com sucesso!");
        }

        [HttpPost]
        public async Task<IActionResult> Post(EmployeeDTO employeeDto)
        {

            Execute.Executar(employeeDto);
            employeeDto.Id = 0;
            await _employeeService.Create(employeeDto);

            return Response<EmployeeDTO>.Created(employeeDto, "Funcionario Cadastrado com sucesso!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EmployeeDTO employeeDto)
        {
            Execute.Executar(employeeDto);
            await _employeeService.Update(employeeDto, id); ;

            return Response<EmployeeDTO>.Ok(employeeDto, "Produto atualizado com sucesso!");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            await _employeeService.Remove(id);

            return Response<object>.NoContent();
        }
    }
}

