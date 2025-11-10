using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Builders
{
    public class ResidentAllergyBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResidentAllergy>().HasKey(ra => ra.Id);

            modelBuilder.Entity<ResidentAllergy>()
                .Property(ra => ra.Description);

            modelBuilder.Entity<ResidentAllergy>()
                .Property(ra => ra.DetectionDate)
                .HasColumnType("date");

            modelBuilder.Entity<ResidentAllergy>()
                .Property(ra => ra.ReleasedDate)
                .HasColumnType("date");

            modelBuilder.Entity<ResidentAllergy>()
                .HasOne(ra => ra.Resident)
                .WithMany(r => r.Allergies)
                .HasForeignKey(ra => ra.ResidentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ResidentAllergy>()
                .HasOne(ra => ra.Allergy)
                .WithMany(a => a.Residents)
                .HasForeignKey(ra => ra.AllergyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ResidentAllergy>().HasData(
                new ResidentAllergy
                {
                    Id = 1,
                    ResidentId = 1,
                    AllergyId = 1,
                    Description = "Teste 01",
                    DetectionDate = new DateTime(2020, 5, 15),
                    ReleasedDate = null
                },
                new ResidentAllergy
                {
                    Id = 2,
                    ResidentId = 1,
                    Description = "Teste 02",
                    AllergyId = 2,
                    DetectionDate = new DateTime(2019, 8, 22),
                    ReleasedDate = null
                },
                new ResidentAllergy
                {
                    Id = 3,
                    ResidentId = 2,
                    AllergyId = 6,
                    Description = "Teste 03",
                    DetectionDate = new DateTime(2021, 3, 10),
                    ReleasedDate = null
                }
            );
        }
    }
}
