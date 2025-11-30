using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Objects.Enums;

namespace SeniorCareManager.WebAPI.Data.Builders
{
    public class ResidentRelativeBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<ResidentRelative>();
            entity.ToTable("resident_relative");
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id);
            entity.Property(x => x.ResidentId).IsRequired();
            entity.Property(x => x.Name).IsRequired().HasMaxLength(150);
            entity.Property(x => x.Cpf).IsRequired().HasMaxLength(11);
            entity.Property(x => x.Rg).IsRequired().HasMaxLength(20);
            entity.Property(x => x.IssuingState).IsRequired().HasMaxLength(2);
            entity.Property(x => x.Relationship).IsRequired();
            entity.Property(x => x.MobileNumber).IsRequired().HasMaxLength(20);
            entity.Property(x => x.HomePhoneNumber).IsRequired(false).HasMaxLength(20);
            entity.Property(x => x.Email).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Street).IsRequired().HasMaxLength(150);
            entity.Property(x => x.Number).IsRequired().HasMaxLength(10);
            entity.Property(x => x.District).IsRequired().HasMaxLength(50);
            entity.Property(x => x.City).IsRequired().HasMaxLength(100);
            entity.Property(x => x.State).IsRequired().HasMaxLength(2);
            entity.Property(x => x.PostalCode).IsRequired().HasMaxLength(8);
            entity.Property(x => x.IssuingBody).IsRequired().HasMaxLength(50);
            entity.Property(x => x.Citizenship).IsRequired().HasMaxLength(50);

            entity.Property(x => x.AddressComplement).IsRequired(false);

            entity.HasOne(x => x.Resident)
                  .WithMany(r => r.Relatives)
                  .HasForeignKey(x => x.ResidentId)
                  .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ResidentRelative>()
            .HasData(new List<ResidentRelative>
            {
                new ResidentRelative
                {
                    Id = 1,
                    ResidentId = 1,
                    Name = "Maria Silva",
                    Cpf = "12345678901",
                    Rg = "12345678",
                    IssuingState = "SP",
                    Relationship = Relationship.SPOUSE,
                    MobileNumber = "5511912345678",
                    HomePhoneNumber = "1134567890",
                    Email = "maria.silva@example.com",
                    Street = "Rua das Flores",
                    Number = "100",
                    AddressComplement = "Apto 12",
                    District = "Centro",
                    City = "São Paulo",
                    State = "SP",
                    PostalCode = "01000000",
                    IssuingBody = "SSP-SP",
                    Citizenship = "Brasileira"
                },
                new ResidentRelative
                {
                    Id = 2,
                    ResidentId = 1,
                    Name = "João Silva",
                    Cpf = "23456789012",
                    Rg = "87654321",
                    IssuingState = "SP",
                    Relationship = Relationship.SON_DAUGHTER,
                    MobileNumber = "5511998765432",
                    HomePhoneNumber = null,
                    Email = "joao.silva@example.com",
                    Street = "Rua das Flores",
                    Number = "100",
                    AddressComplement = null,
                    District = "Centro",
                    City = "São Paulo",
                    State = "SP",
                    PostalCode = "01000000",
                    IssuingBody = "SSP-SP",
                    Citizenship = "Brasileira"
                },
                new ResidentRelative
                {
                    Id = 3,
                    ResidentId = 2,
                    Name = "Carlos Souza",
                    Cpf = "34567890123",
                    Rg = "11223344",
                    IssuingState = "RJ",
                    Relationship = Relationship.OTHER,
                    MobileNumber = "5521977771111",
                    HomePhoneNumber = null,
                    Email = "carlos.souza@example.com",
                    Street = "Av. Atlântica",
                    Number = "45B",
                    AddressComplement = null,
                    District = "Copacabana",
                    City = "Rio de Janeiro",
                    State = "RJ",
                    PostalCode = "22010000",
                    IssuingBody = "SSP-RJ",
                    Citizenship = "Brasileira"
                }
            });
        }
    }
}
