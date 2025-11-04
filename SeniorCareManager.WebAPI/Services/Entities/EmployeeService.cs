using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Services.Entities
{
    public class EmployeeService : GenericService<Employee, EmployeeDTO>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _employeeRepository = repository;
            _mapper = mapper;
        }

        public async Task<EmployeeDTO> GetById(int id)
        {
            var employee = await _employeeRepository.GetById(id);
            if (employee is null)
                throw new ExceptionNotFound("Funvionario com o id " + id + " informado n�o foi encontrado.");

            return _mapper.Map<EmployeeDTO>(employee);
        }

        public async Task Update(EmployeeDTO employeeDTO, int id)
        {
            var employee = _mapper.Map<Employee>(employeeDTO);
            var existinemployee = await _employeeRepository.GetById(id); // Supondo que sua entidade tenha um campo Id

            if (employee == null)
            {
                throw new ExceptionNotFound($"Funcionario com id {id} n�o encontrado!");
            }

            if (await CheckDuplicates(employeeDTO.Cpf))
                throw new ExceptionConflict("CPF duplicado.");

            await _employeeRepository.Update(employee);
        }


        public async Task Remove(int id)
        {
            var employee = await _employeeRepository.GetById(id);
            if (employee == null)
            {
                throw new ExceptionNotFound($"Entidade com id: {id} n�o encontrado");
            }

            await _employeeRepository.Remove(employee);
        }

        public async Task<bool> CheckDuplicates(string cpf)
        {
            var employees = await _employeeRepository.Get();
            return employees.Any(r => StringValidator.CompareString(r.Cpf, cpf));
        }
    }
}
