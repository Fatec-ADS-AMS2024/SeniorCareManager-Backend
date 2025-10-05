using SeniorCareManager.WebAPI.Objects.Enums;
using SeniorCareManager.WebAPI.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace SeniorCareManager.WebAPI.Data.Builders
{
    public class ResidentBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resident>().HasKey(r => r.Id);
            modelBuilder.Entity<Resident>()
                .Property(r => r.RegisteredName)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Resident>()
                .Property(r => r.SocialName)
                .HasMaxLength(100);
            modelBuilder.Entity<Resident>()
                .Property(r => r.DateOfBirth)
                .IsRequired();
            modelBuilder.Entity<Resident>()
                .Property(r => r.Cpf)
                .IsRequired()
                .HasMaxLength(11);
            modelBuilder.Entity<Resident>()
                .Property(r => r.Rg)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<Resident>()
                .Property(r => r.IssuingBody)
                .HasMaxLength(20);
            modelBuilder.Entity<Resident>()
                .Property(r => r.IssuingState)
                .HasMaxLength(2);
            modelBuilder.Entity<Resident>()
                .Property(r => r.PisPasep)
                .HasMaxLength(11);
            modelBuilder.Entity<Resident>()
                .Property(r => r.FatherName)
                .HasMaxLength(100);
            modelBuilder.Entity<Resident>()
                .Property(r => r.MotherName)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Resident>()
                .Property(r => r.SpouseName)
                .HasMaxLength(100);
            modelBuilder.Entity<Resident>()
                .Property(r => r.NationalHealthCardNumber)
                .HasMaxLength(15);
            modelBuilder.Entity<Resident>()
                .Property(r => r.PrivateHealthCardNumber)
                .HasMaxLength(20);
            modelBuilder.Entity<Resident>()
                .Property(r => r.MobileNumber)
                .HasMaxLength(11);
            modelBuilder.Entity<Resident>()
                .Property(r => r.HomePhoneNumber)
                .HasMaxLength(11);
            modelBuilder.Entity<Resident>()
                .Property(r => r.Height)
                .HasColumnType("decimal(5,2)");
            modelBuilder.Entity<Resident>()
                .Property(r => r.Weight)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<Resident>()
                .HasData(new List<Resident>
                {
                    new Resident(1, "João da Silva", "João", new DateTime(1940, 5, 20), "83", "12345678901", "MG1234567", "SSP", "MG", "12345678901", Sex.FEMALE, MaritalStatus.MARRIED, Ethnicity.WHITE, "Carlos da Silva", "Maria de Souza", "Ana Silva", "123456789012345", "9876543210", "31999999999", "3133333333", 1.75m, 80.5m),
                    new Resident(2, "Maria de Souza", "Maria", new DateTime(1935, 8, 15), "88", "10987654321", "SP7654321", "SSP", "SP", "1098765654", Sex.MALE, MaritalStatus.DIVORCED, Ethnicity.WHITE, "José de Souza", "Ana Pereira", "Carlos Souza", "543216789012345", "1234567890", "21988888888", "2133333333", 1.60m, 65.0m),
                    new Resident(3, "Ana Pereira", "Ana", new DateTime(1945, 12, 30), "77", "11223344556", "RJ1122334", "SSP", "RJ", "1122334451", Sex.FEMALE, MaritalStatus.SEPARATED, Ethnicity.WHITE, "Pedro Pereira", "Clara Lima", "João Pereira", "678905432109876", "5678901234", "31977777777", "3132222222", 1.68m, 70.0m)
                });
        }
    }
}
