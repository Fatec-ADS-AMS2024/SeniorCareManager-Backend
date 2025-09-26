using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Builders
{
    public class ResidentAllergyBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ResidentAllergy>().HasKey(pg => pg.Id);
            modelBuilder.Entity<ResidentAllergy>().Property(pg => pg.Description)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<ResidentAllergy>()
                .HasOne(ra => ra.Resident)
                .WithMany(r => r.Allergies)
                .HasForeignKey(ra => ra.ResidentId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ResidentAllergy>()
                .HasIndex(ra => new { ra.ResidentId, ra.Description })
                .IsUnique();
            modelBuilder.Entity<ResidentAllergy>()
                .Property(ra => ra.DetectionDate)
                .HasColumnType("date");
            modelBuilder.Entity<ResidentAllergy>()
                .Property(ra => ra.ReleasedDate)
                .HasColumnType("date");

            modelBuilder.Entity < ResidentAllergy >().HasData(
                new ResidentAllergy
                {
                    Id = 1,
                    ResidentId = 1,
                    Description = "Penicillin",
                    DetectionDate = new DateTime(2020, 5, 15),
                    ReleasedDate = null
                },
                new ResidentAllergy
                {
                    Id = 2,
                    ResidentId = 1,
                    Description = "Peanuts",
                    DetectionDate = new DateTime(2019, 8, 22),
                    ReleasedDate = null
                },
                new ResidentAllergy
                {
                    Id = 3,
                    ResidentId = 2,
                    Description = "Latex",
                    DetectionDate = new DateTime(2021, 3, 10),
                    ReleasedDate = null
                }
            );
        }
    }
}
