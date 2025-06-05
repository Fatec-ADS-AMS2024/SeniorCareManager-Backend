using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Builders
{
    public class CompanyBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasKey(c => c.Id);

            modelBuilder.Entity<Company>()
                .Property(c => c.CompanyName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Company>()
                .Property(c => c.TradeName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Company>()
                .Property(c => c.CNPJ)
                .IsRequired()
                .HasMaxLength(14);

            modelBuilder.Entity<Company>()
                .Property(c => c.Email)
                .HasMaxLength(100);

            modelBuilder.Entity<Company>()
                .Property(c => c.Street)
                .HasMaxLength(100);

            modelBuilder.Entity<Company>()
                .Property(c => c.Number)
                .HasMaxLength(10);

            modelBuilder.Entity<Company>()
                .Property(c => c.District)
                .HasMaxLength(50);

            modelBuilder.Entity<Company>()
                .Property(c => c.AddressComplement)
                .HasMaxLength(100);

            modelBuilder.Entity<Company>()
                .Property(c => c.City)
                .HasMaxLength(50);

            modelBuilder.Entity<Company>()
                .Property(c => c.State)
                .HasMaxLength(2);

            modelBuilder.Entity<Company>()
                .Property(c => c.PostalCode)
                .HasMaxLength(8);

            modelBuilder.Entity<Company>()
                .Property(c => c.CompanyLogo)
                .HasMaxLength(200);

            // Inserção de dados iniciais
            modelBuilder.Entity<Company>().HasData(new List<Company>
            {
                new Company(1, "Empresa A", "Trade A", "12345678000195", "empresa1@gmail.com", "Rua A", "123", "Bairro A", "Complemento A", "Cidade A", "SP", "12345678", "logoA.png"),
                new Company(2, "Empresa B", "Trade B", "12345678000196", "empresa2@gmail.com", "Rua B", "456", "Bairro B", "Complemento B", "Cidade B", "RJ", "23456789", "logoB.png"),
                new Company(3, "Empresa C", "Trade C", "12345678000197", "empresa3@gmail.com", "Rua C", "789", "Bairro C", "Complemento C", "Cidade C", "MG", "34567890", "logoC.png")
            });
        }
    }
}