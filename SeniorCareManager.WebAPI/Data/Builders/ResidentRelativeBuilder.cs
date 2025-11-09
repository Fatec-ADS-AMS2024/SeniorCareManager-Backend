using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Builders
{
    public class ResidentRelativeBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<ResidentRelative>();
            entity.ToTable("residentrelative");
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id);
            entity.Property(x => x.ResidentId).IsRequired();
            entity.Property(x => x.Name).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Relationship).IsRequired();
            entity.Property(x => x.Citizenship).IsRequired().HasMaxLength(50);
            entity.Property(x => x.MobileNumber).IsRequired().HasMaxLength(20);
            entity.Property(x => x.HomePhoneNumber).IsRequired(false).HasMaxLength(20);
            entity.Property(x => x.Email).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Street).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Number).IsRequired().HasMaxLength(10);
            entity.Property(x => x.City).IsRequired().HasMaxLength(50);
            entity.Property(x => x.State).IsRequired().HasMaxLength(50);
            entity.Property(x => x.PostalCode).IsRequired().HasMaxLength(20);
            entity.Property(x => x.IssuingBody).IsRequired().HasMaxLength(50);

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
