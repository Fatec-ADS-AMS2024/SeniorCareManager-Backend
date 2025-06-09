using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Dtos;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var suppliers = await _supplierService.GetAll();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var supplier = await _supplierService.GetById(id);
            if (supplier == null)
                return NotFound("Fornecedor não encontrado!");

            return Ok(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Post(SupplierDTO supplier)
        {
            // Remove máscaras de CpfCnpj e Phone
            supplier.CpfCnpj = StringValidator.ExtractNumbers(supplier.CpfCnpj);
            supplier.Phone = StringValidator.ExtractNumbers(supplier.Phone);

            var existingSuppliers = await _supplierService.GetAll();

            // Verifica duplicidade de CNPJ
            if (existingSuppliers.Any(s => StringValidator.ExtractNumbers(s.CpfCnpj) == supplier.CpfCnpj))
                return BadRequest("Já existe um fornecedor com este CPF/CNPJ.");

            // Verifica duplicidade de razão social (corporate name), ignorando acento e case
            if (existingSuppliers.Any(s => StringValidator.CompareString(s.CorporateName, supplier.CorporateName)))
                return BadRequest("Já existe um fornecedor com esta razão social.");

            // Validações manuais já existentes
            if (string.IsNullOrWhiteSpace(supplier.CorporateName))
                return BadRequest("A razão social do fornecedor é obrigatória.");

            if (!StringValidator.ContainsOnlyLettersAndSpaces(supplier.TradeName))
                return BadRequest("O nome fantasia deve conter apenas letras e espaços.");

            if (!EmailValidator.IsValid(supplier.Email))
                return BadRequest("O e-mail informado é inválido.");

            if (!PhoneValidator.IsValidPhoneNumber(supplier.Phone))
                return BadRequest("O telefone informado é inválido. Deve conter DDD e ter 10 ou 11 dígitos.");

            if (!CpfCnpjValidator.IsValidCNPJ(supplier.CpfCnpj))
                return BadRequest("O CPF ou CNPJ informado é inválido.");

            // Validações adicionais para endereço
            if (string.IsNullOrWhiteSpace(supplier.PostalCode) || !StringValidator.IsNumeric(supplier.PostalCode) || supplier.PostalCode.Length != 8)
                return BadRequest("O CEP informado é inválido. Deve conter exatamente 8 números.");

            if (string.IsNullOrWhiteSpace(supplier.Street))
                return BadRequest("O nome da rua é obrigatório.");

            if (string.IsNullOrWhiteSpace(supplier.Number))
                return BadRequest("O número do endereço é obrigatório.");

            if (!StringValidator.ContainsOnlyLettersNumbersSpaces(supplier.Number))
                return BadRequest("O número do endereço deve conter apenas letras, números e espaços.");

            if (string.IsNullOrWhiteSpace(supplier.District))
                return BadRequest("O bairro é obrigatório.");

            if (!StringValidator.ContainsOnlyLettersAndSpaces(supplier.District))
                return BadRequest("O bairro deve conter apenas letras e espaços.");

            if (string.IsNullOrWhiteSpace(supplier.City))
                return BadRequest("A cidade é obrigatória.");

            if (!StringValidator.ContainsOnlyLettersAndSpaces(supplier.City))
                return BadRequest("A cidade deve conter apenas letras e espaços.");

            if (!StringValidator.IsValidUF(supplier.State))
                return BadRequest("O estado informado é inválido.");

            try
            {
                await _supplierService.Create(supplier);
                return Ok(supplier);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar inserir um novo fornecedor: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, SupplierDTO supplier)
        {
            // Remove máscaras de CpfCnpj e Phone
            supplier.CpfCnpj = StringValidator.ExtractNumbers(supplier.CpfCnpj);
            supplier.Phone = StringValidator.ExtractNumbers(supplier.Phone);

            var existingSuppliers = await _supplierService.GetAll();

            // Verifica duplicidade de CNPJ (exceto o fornecedor que está sendo atualizado)
            if (existingSuppliers.Any(s => s.Id != id && StringValidator.ExtractNumbers(s.CpfCnpj) == supplier.CpfCnpj))
                return BadRequest("Já existe outro fornecedor com este CPF/CNPJ.");

            // Verifica duplicidade de razão social (exceto o fornecedor que está sendo atualizado)
            if (existingSuppliers.Any(s => s.Id != id && StringValidator.CompareString(s.CorporateName, supplier.CorporateName)))
                return BadRequest("Já existe outro fornecedor com esta razão social.");

            // Validações manuais já existentes
            if (string.IsNullOrWhiteSpace(supplier.CorporateName))
                return BadRequest("A razão social do fornecedor é obrigatória.");

            if (!StringValidator.ContainsOnlyLettersAndSpaces(supplier.TradeName))
                return BadRequest("O nome fantasia deve conter apenas letras e espaços.");

            if (!EmailValidator.IsValid(supplier.Email))
                return BadRequest("O e-mail informado é inválido.");

            if (!PhoneValidator.IsValidPhoneNumber(supplier.Phone))
                return BadRequest("O telefone informado é inválido. Deve conter DDD e ter 10 ou 11 dígitos.");

            if (!CpfCnpjValidator.IsValidCNPJ(supplier.CpfCnpj))
                return BadRequest("O CPF ou CNPJ informado é inválido.");

            // Validações adicionais para endereço
            if (string.IsNullOrWhiteSpace(supplier.PostalCode) || !StringValidator.IsNumeric(supplier.PostalCode) || supplier.PostalCode.Length != 8)
                return BadRequest("O CEP informado é inválido. Deve conter exatamente 8 números.");

            if (string.IsNullOrWhiteSpace(supplier.Street))
                return BadRequest("O nome da rua é obrigatório.");

            if (string.IsNullOrWhiteSpace(supplier.Number))
                return BadRequest("O número do endereço é obrigatório.");

            if (!StringValidator.ContainsOnlyLettersNumbersSpaces(supplier.Number))
                return BadRequest("O número do endereço deve conter apenas letras, números e espaços.");

            if (string.IsNullOrWhiteSpace(supplier.District))
                return BadRequest("O bairro é obrigatório.");

            if (!StringValidator.ContainsOnlyLettersAndSpaces(supplier.District))
                return BadRequest("O bairro deve conter apenas letras e espaços.");

            if (string.IsNullOrWhiteSpace(supplier.City))
                return BadRequest("A cidade é obrigatória.");

            if (!StringValidator.ContainsOnlyLettersAndSpaces(supplier.City))
                return BadRequest("A cidade deve conter apenas letras e espaços.");

            if (!StringValidator.IsValidUF(supplier.State))
                return BadRequest("O estado informado é inválido.");

            try
            {
                await _supplierService.Update(supplier, id);
                return Ok(supplier);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar atualizar o fornecedor: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _supplierService.Remove(id);
                return Ok("Fornecedor removido com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar remover o fornecedor: " + ex.Message);
            }
        }
    }
}
