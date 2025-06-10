using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeniorCareManager.WebAPI.Objects.Dtos
{
    public class SupplierDTO
    {
        public int Id { get; set; }
        public string CorporateName { get; set; }
        public string TradeName { get; set; }
        public string CpfCnpj { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string District { get; set; }
        public string AddressComplement { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public async Task<(bool IsValid, string ErrorMessage)> ValidateAsync(ISupplierService service, bool isUpdate = false)
        {
            CpfCnpj = StringValidator.ExtractNumbers(CpfCnpj);
            Phone = StringValidator.ExtractNumbers(Phone);

            if (await service.ExistsByCpfCnpj(CpfCnpj, isUpdate ? Id : null))
                return (false, "Já existe um fornecedor com este CPF/CNPJ.");

            if (await service.ExistsByCorporateName(CorporateName, isUpdate ? Id : null))
                return (false, "Já existe um fornecedor com esta razão social.");

            if (string.IsNullOrWhiteSpace(CorporateName))
                return (false, "A razão social do fornecedor é obrigatória.");

            if (!StringValidator.ContainsOnlyLettersAndSpaces(TradeName))
                return (false, "O nome fantasia deve conter apenas letras e espaços.");

            if (!EmailValidator.IsValid(Email))
                return (false, "O e-mail informado é inválido.");

            if (!PhoneValidator.IsValidPhoneNumber(Phone))
                return (false, "O telefone informado é inválido. Deve conter DDD e ter 10 ou 11 dígitos.");

            if (!CpfCnpjValidator.IsValidCNPJ(CpfCnpj))
                return (false, "O CPF ou CNPJ informado é inválido.");

            if (string.IsNullOrWhiteSpace(PostalCode) || !StringValidator.IsNumeric(PostalCode) || PostalCode.Length != 8)
                return (false, "O CEP informado é inválido. Deve conter exatamente 8 números.");

            if (string.IsNullOrWhiteSpace(Street))
                return (false, "O nome da rua é obrigatório.");

            if (string.IsNullOrWhiteSpace(Number))
                return (false, "O número do endereço é obrigatório.");

            if (!StringValidator.ContainsOnlyLettersNumbersSpaces(Number))
                return (false, "O número do endereço deve conter apenas letras, números e espaços.");

            if (string.IsNullOrWhiteSpace(AddressComplement))
                return (false, "O complemento do endereço é obrigatório.");

            if (string.IsNullOrWhiteSpace(District))
                return (false, "O bairro é obrigatório.");

            if (!StringValidator.ContainsOnlyLettersAndSpaces(District))
                return (false, "O bairro deve conter apenas letras e espaços.");

            if (string.IsNullOrWhiteSpace(City))
                return (false, "A cidade é obrigatória.");

            if (!StringValidator.ContainsOnlyLettersAndSpaces(City))
                return (false, "A cidade deve conter apenas letras e espaços.");

            if (!StringValidator.IsValidUF(State))
                return (false, "O estado informado é inválido.");

            return (true, null);
        }
    }
}