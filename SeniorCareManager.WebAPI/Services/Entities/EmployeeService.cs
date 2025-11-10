using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Data;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Data.Repositories;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
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
        private readonly AppDbContext _context;

        public EmployeeService(IEmployeeRepository repository, AppDbContext context, IMapper mapper) : base(repository, mapper)
        {
            _employeeRepository = repository;
            _mapper = mapper;
        }

        public async Task<EmployeeDTO> GetById(int id)
        {
            var employee = await _employeeRepository.GetById(id);
            if (employee is null)
                throw new ExceptionNotFound("Funcionario com o id " + id + " informado n�o foi encontrado.");

            return _mapper.Map<EmployeeDTO>(employee);
        }

        public async Task<EmployeeDTO> Create(EmployeeDTO employeeDTO, int id)
        {
            var employee = _mapper.Map<Employee>(employeeDTO);
            var errors = new List<FieldError>();
            var isPositionExist = await _context.Set<Position>().AnyAsync(ra => ra.Id == id);

            if (isPositionExist)
            {
                throw new InvalidOperationException("Esse cargo não existe.");
            }
   

            if (await CheckDuplicates(p => p.Cpf, employeeDTO.Cpf, employeeDTO.Id))
                errors.Add(new FieldError { Field = "Cpf", Message = "Cpf já cadastrado" });

            if (await CheckDuplicates(p => p.Phone, employeeDTO.Phone, employeeDTO.Id))
                errors.Add(new FieldError { Field = "Email", Message = "Telefone duplicado" });

            return _mapper.Map<EmployeeDTO>(await base.Create(employeeDTO));
        }

        public async Task Update(EmployeeDTO employeeDTO, int id)
        {
            var employee = _mapper.Map<Employee>(employeeDTO);
            var existinemployee = await _employeeRepository.GetById(id); // Supondo que sua entidade tenha um campo Id
            var errors = new List<FieldError>();
            var isPositionExist = await _context.Set<Position>().AnyAsync(ra => ra.Id == id);

            if (isPositionExist)
                throw new InvalidOperationException("Esse cargo não existe.");

            if (employee.Id != id)
                throw new ExceptionBadRequest("O id da religião dever ser o mesmo.");

            if (employee == null)
            {
                throw new ExceptionNotFound($"Funcionario com id {id} n�o encontrado!");
            }

            if (await CheckDuplicates(p => p.Cpf, employeeDTO.Cpf, employeeDTO.Id))
                errors.Add(new FieldError { Field = "Cpf", Message = "Cpf duplicado" });

            if (await CheckDuplicates(p => p.Email, employeeDTO.Email, employeeDTO.Id))
                errors.Add(new FieldError { Field = "Email", Message = "E-mail duplicado" });

            if (await CheckDuplicates(p => p.Phone, employeeDTO.Phone, employeeDTO.Id))
                errors.Add(new FieldError { Field = "Email", Message = "Telefone duplicado" });

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

        public async Task<bool> CheckDuplicates(Func<Employee, string?> selector, string? valor, int idIgnor)
        {
            var planos = await _employeeRepository.Get(); 
            return planos.Any(p =>
                p.Id != idIgnor &&
                StringUtils.CompareString(selector(p)!, valor)
            );
        }
    }
}
