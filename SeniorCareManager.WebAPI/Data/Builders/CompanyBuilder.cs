using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Builders
{
    public static class CompanyBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Company>();

            entity.HasKey(c => c.Id);

            entity.Property(c => c.CompanyName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(c => c.TradeName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(c => c.CNPJ)
                .IsRequired()
                .HasMaxLength(14);

            entity.Property(c => c.Email)
                .HasMaxLength(100);

            entity.Property(c => c.Street)
                .HasMaxLength(100);

            entity.Property(c => c.Number)
                .HasMaxLength(10);

            entity.Property(c => c.District)
                .HasMaxLength(50);

            entity.Property(c => c.AddressComplement)
                .HasMaxLength(100);

            entity.Property(c => c.City)
                .HasMaxLength(50);

            entity.Property(c => c.State)
                .HasMaxLength(2);

            entity.Property(c => c.PostalCode)
                .HasMaxLength(8);

            // CompanyLogo agora é byte[] — não precisa de HasMaxLength
            entity.Property(c => c.CompanyLogo)
                .HasColumnType("BYTEA"); // PostgreSQL tipo binário

            entity.HasData(new List<Company>
            {
                new Company
                {
                    Id = 1,
                    CompanyName = "Empresa A",
                    TradeName = "Trade A",
                    CNPJ = "12345678000195",
                    Email = "empresa1@gmail.com",
                    Street = "Rua A",
                    Number = "123",
                    District = "Bairro A",
                    AddressComplement = "Complemento A",
                    City = "Cidade A",
                    State = "SP",
                    PostalCode = "12345678",
                    CompanyLogo = null
                },
                new Company
                {
                    Id = 2,
                    CompanyName = "Empresa B",
                    TradeName = "Trade B",
                    CNPJ = "12345678000196",
                    Email = "empresa2@gmail.com",
                    Street = "Rua B",
                    Number = "456",
                    District = "Bairro B",
                    AddressComplement = "Complemento B",
                    City = "Cidade B",
                    State = "RJ",
                    PostalCode = "23456789",
                    CompanyLogo = null
                },
                new Company
                {
                    Id = 3,
                    CompanyName = "Empresa C",
                    TradeName = "Trade C",
                    CNPJ = "12345678000197",
                    Email = "empresa3@gmail.com",
                    Street = "Rua C",
                    Number = "789",
                    District = "Bairro C",
                    AddressComplement = "Complemento C",
                    City = "Cidade C",
                    State = "MG",
                    PostalCode = "34567890",
                    CompanyLogo = null
                }
            });
        }
    }
}
