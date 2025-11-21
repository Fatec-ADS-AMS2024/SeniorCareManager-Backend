using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Builders;

public class SupplierBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        // Configura a chave primária
        modelBuilder.Entity<Supplier>().HasKey(s => s.Id);

        modelBuilder.Entity<Supplier>().Property(s => s.CorporateName)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Supplier>().Property(s => s.TradeName)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Supplier>().Property(s => s.CpfCnpj)
            .IsRequired()
            .HasMaxLength(14);

        modelBuilder.Entity<Supplier>().Property(s => s.Email)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Supplier>().Property(s => s.Phone)
            .IsRequired()
            .HasMaxLength(11);

        modelBuilder.Entity<Supplier>().Property(s => s.PostalCode)
            .IsRequired()
            .HasMaxLength(8);

        modelBuilder.Entity<Supplier>().Property(s => s.Street)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Supplier>().Property(s => s.Number)
            .IsRequired()
            .HasMaxLength(5);

        modelBuilder.Entity<Supplier>().Property(s => s.District)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Supplier>().Property(s => s.AddressComplement)
            .HasMaxLength(100);

        modelBuilder.Entity<Supplier>().Property(s => s.City)
            .IsRequired()
            .HasMaxLength(100);

            modelBuilder.Entity<Supplier>().Property(s => s.State)
                .IsRequired()
                .HasMaxLength(2);

            modelBuilder.Entity<Supplier>().HasData(new List<Supplier>
            {
                new (
                    id: 1,
                    corporateName: "FarmaVida Distribuidora Ltda",
                    tradeName: "FarmaVida",
                    cpfCnpj: "12345678000190", // ✅ Sem pontuação
                    email: "vendas@farmavida.com.br",
                    phone: "1134567890",
                    postalCode: "01234567",
                    street: "Rua das Farmácias",
                    number: "100",
                    district: "Centro",
                    addressComplement: "Sala 201",
                    city: "São Paulo",
                    state: "SP"
                ),
                new (
                    id: 2,
                    corporateName: "MedEquip Comércio de Equipamentos Hospitalares S.A.",
                    tradeName: "MedEquip",
                    cpfCnpj: "98765432000110", // ✅ Sem pontuação
                    email: "compras@medequip.com.br",
                    phone: "1123456789",
                    postalCode: "04567890",
                    street: "Avenida dos Hospitais",
                    number: "500",
                    district: "Jardim Paulista",
                    addressComplement: "Bloco B",
                    city: "São Paulo",
                    state: "SP"
                ),
                new (
                    id: 3,
                    corporateName: "NutriSenior Alimentos Especiais Ltda",
                    tradeName: "NutriSenior",
                    cpfCnpj: "55666777000188", // ✅ Sem pontuação
                    email: "atendimento@nutrisenior.com.br",
                    phone: "1145678901",
                    postalCode: "07890123",
                    street: "Rua da Nutrição",
                    number: "250",
                    district: "Vila Mariana",
                    addressComplement: "Galpão 3",
                    city: "São Paulo",
                    state: "SP"
                )
            });
        }
    }
}
