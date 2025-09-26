using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Builders
{
    public class ResidentRelativeBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            // Configura a chave primária
            modelBuilder.Entity<ResidentRelative>().HasKey(rr => rr.Id);
            modelBuilder.Entity<ResidentRelative>()
                .Property(rr => rr.Name)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<ResidentRelative>()
                .Property(rr => rr.Citizenship)
                .HasMaxLength(50);
            modelBuilder.Entity<ResidentRelative>()
                .Property(rr => rr.MobileNumber)
                .HasMaxLength(20);
            modelBuilder.Entity<ResidentRelative>()
                .Property(rr => rr.HomePhoneNumber)
                .HasMaxLength(20);
            modelBuilder.Entity<ResidentRelative>()
                .Property(rr => rr.Email)
                .HasMaxLength(100);
            modelBuilder.Entity<ResidentRelative>()
                .Property(rr => rr.Street)
                .HasMaxLength(100);
            modelBuilder.Entity<ResidentRelative>()
                .Property(rr => rr.Number)
                .HasMaxLength(10);
            modelBuilder.Entity<ResidentRelative>()
                .Property(rr => rr.AddressComplement)
                .HasMaxLength(50);
            modelBuilder.Entity<ResidentRelative>()
                .Property(rr => rr.City)
                .HasMaxLength(50);
            modelBuilder.Entity<ResidentRelative>()
                .Property(rr => rr.State)
                .HasMaxLength(50);
            modelBuilder.Entity<ResidentRelative>()
                .Property(rr => rr.PostalCode)
                .HasMaxLength(20);
            modelBuilder.Entity<ResidentRelative>()
                .Property(rr => rr.IssuingBody)
                .HasMaxLength(50);
            // Configura o relacionamento com Resident
            modelBuilder.Entity<ResidentRelative>()
                .HasOne(rr => rr.Resident)
                .WithMany(r => r.Relatives)
                .HasForeignKey(rr => rr.ResidentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ResidentRelative>()
                .HasData(new List<ResidentRelative>
                {
                    new ResidentRelative
                    {
                        Id = 1,
                        ResidentId = 1,
                        Name = "Maria Silva",
                        Citizenship = "Brasileira",
                        MobileNumber = "+55 11 91234-5678",
                        HomePhoneNumber = "11 4002-8922",
                        Email = "maria.silva@example.com",
                        Street = "Rua das Flores",
                        Number = "100",
                        AddressComplement = "Apto 12",
                        City = "São Paulo",
                        State = "SP",
                        PostalCode = "01000-000",
                        IssuingBody = "SSP-SP"
                    },
                    new ResidentRelative
                    {
                        Id = 2,
                        ResidentId = 1,
                        Name = "João Silva",
                        Citizenship = "Brasileira",
                        MobileNumber = "+55 11 99876-5432",
                        Email = "joao.silva@example.com",
                        Street = "Rua das Flores",
                        Number = "100",
                        City = "São Paulo",
                        State = "SP",
                        PostalCode = "01000-000",
                        IssuingBody = "SSP-SP"
                    },
                    new ResidentRelative
                    {
                        Id = 3,
                        ResidentId = 2,
                        Name = "Carlos Souza",
                        Citizenship = "Brasileira",
                        MobileNumber = "+55 21 97777-1111",
                        Email = "carlos.souza@example.com",
                        Street = "Av. Atlântica",
                        Number = "45B",
                        City = "Rio de Janeiro",
                        State = "RJ",
                        PostalCode = "22010-000",
                        IssuingBody = "SSP-RJ"
                    }
                });
        }
    }
}
