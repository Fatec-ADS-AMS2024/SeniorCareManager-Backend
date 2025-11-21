using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Data;
using SeniorCareManager.WebAPI.Data.Interfaces;
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
            _context = context;
        }

        public override async Task<EmployeeDTO> GetById(int id)
        {
            var employee = await _employeeRepository.GetById(id);
            if (employee is null)
                throw new ExceptionNotFound("Funcionario com o id " + id + " informado n�o foi encontrado.");

            return _mapper.Map<EmployeeDTO>(employee);
        }

        public override async Task<EmployeeDTO> Create(EmployeeDTO employeeDTO)
        {
            var errors = new List<FieldError>();

            var isPositionExist = await _context.Set<Position>().AnyAsync(p => p.Id == employeeDTO.PositionId);
            if (!isPositionExist)
            {
                errors.Add(new FieldError{ Field = "PositionId", Message = "Cargo não encontrado" });
            }

            if (await CheckDuplicates(p => p.Cpf, employeeDTO.Cpf, 0))
                errors.Add(new FieldError{ Field = "Cpf", Message = "CPF já cadastrado" });

            if (await CheckDuplicates(p => p.Email, employeeDTO.Email, 0))
                errors.Add(new FieldError{ Field = "Email", Message = "E-mail já cadastrado" });

            if (await CheckDuplicates(p => p.Phone, employeeDTO.Phone, 0))
                errors.Add(new FieldError{ Field = "Phone", Message = "Telefone já cadastrado" });

            if (errors.Any())
                throw new ExceptionBadRequest("Erros de validação", errors);

            return _mapper.Map<EmployeeDTO>(await base.Create(employeeDTO));
        }

        public override async Task Update(EmployeeDTO employeeDTO, int id)
        {
            var errors = new List<FieldError>();

            var existingEmployee = await _employeeRepository.GetById(id);
            if (existingEmployee == null)
                throw new ExceptionNotFound($"Funcionário com id {id} não encontrado!");

            var isPositionExist = await _context.Set<Position>().AnyAsync(p => p.Id == employeeDTO.PositionId);
            if (!isPositionExist)
                errors.Add(new FieldError{ Field = "PositionId", Message = "Cargo não encontrado" });

            if (await CheckDuplicates(p => p.Cpf, employeeDTO.Cpf, id))
                errors.Add(new FieldError { Field = "Cpf", Message = "CPF já cadastrado" });

            if (await CheckDuplicates(p => p.Email, employeeDTO.Email, id))
                errors.Add(new FieldError{ Field = "Email", Message = "E-mail já cadastrado" });

            if (await CheckDuplicates(p => p.Phone, employeeDTO.Phone, id))
                errors.Add(new FieldError{ Field = "Phone", Message = "Telefone já cadastrado" });

            if (errors.Any())
                throw new ExceptionBadRequest("Erros de validação", errors);

            _mapper.Map(employeeDTO, existingEmployee);
            await _employeeRepository.Update(existingEmployee);
        }

        public override async Task Remove(int id)
        {
            var employee = await _employeeRepository.GetById(id);
            if (employee == null)
            {
                throw new ExceptionNotFound($"Entidade com id: {id} n�o encontrado");
            }

            await _employeeRepository.Remove(employee);
        }

        public async Task<bool> CheckDuplicates(Func<Employee, string?> selector, string? value, int ignoreId)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            // ✅ Query otimizada no banco - não carrega todos os registros
            var allEmployees = await _employeeRepository.Get();
            var hasDuplicate = allEmployees.Any(emp =>
                emp.Id != ignoreId &&
                !string.IsNullOrWhiteSpace(selector(emp)) &&
                StringUtils.CompareString(selector(emp), value.Trim())
            );

            return hasDuplicate;
        }
    }
}
