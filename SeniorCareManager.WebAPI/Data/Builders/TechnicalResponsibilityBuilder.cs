using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Builders
{
    public static class TechnicalResponsibilityBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<TechnicalResponsibility>();

            // Configura a chave primária
            entity.HasKey(tr => tr.Id);

            entity.Property(tr => tr.ResponsibleName)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(tr => tr.ProfessionalRegistration)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(tr => tr.ServicesResponsibility)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(tr => tr.StartDate)
                  .IsRequired();

            entity.Property(tr => tr.EndDate)
                  .IsRequired();

            modelBuilder.Entity<TechnicalResponsibility>()
                .HasOne(e => e.Employee)                                // 1 responsabilidade TEM 1 funcionario
                .WithMany(p => p.TechnicalResponsibilities)             // 1 funcionario TEM MUITAS responsabilidades
                .HasForeignKey(e => e.EmployeeId)                       // FK na tabela TechnicalResponsibility
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TechnicalResponsibility>()
                .HasOne(e => e.Position)                                // 1 responsabilidade TEM 1 cargo
                .WithMany(p => p.TechnicalResponsibilities)             // 1 cargo TEM MUITOS responsabilidade
                .HasForeignKey(e => e.PositionId)                       // FK na tabela TechnicalResponsibility
                .OnDelete(DeleteBehavior.Restrict);
            /*
            modelBuilder.Entity<TechnicalResponsibility>()
                .HasOne(e => e.Company)                                 // 1 responsabilidade TEM 1 empresa
                .WithMany(p => p.TechnicalResponsibilities)             // 1 empresa TEM MUITAS responsabilidades
                .HasForeignKey(e => e.CompanyId)                        // FK na tabela TechnicalResponsibility
                .OnDelete(DeleteBehavior.Restrict);
            */

            // Inserção de dados iniciais (opcional)
            modelBuilder.Entity<TechnicalResponsibility>()
                .HasData(new List<TechnicalResponsibility>
                {
                    new TechnicalResponsibility
                    {
                        Id = 1,
                        ResponsibleName = "Maria Oliveira",
                        ProfessionalRegistration = "REG12345",
                        ServicesResponsibility = "Supervisão de enfermagem",
                        StartDate = DateTime.SpecifyKind(new DateTime(2023, 01, 01), DateTimeKind.Utc),
                        EndDate = DateTime.SpecifyKind(new DateTime(2025, 12, 31), DateTimeKind.Utc),
                        PositionId = 1,
                        EmployeeId = 1,
                    },
                    new TechnicalResponsibility
                    {
                        Id = 2,
                        ResponsibleName = "João Silva",
                        ProfessionalRegistration = "REG67890",
                        ServicesResponsibility = "Coordenação de cuidados",
                        StartDate = DateTime.SpecifyKind(new DateTime(2024, 03, 15), DateTimeKind.Utc),
                        EndDate = DateTime.SpecifyKind(new DateTime(2026, 03, 14), DateTimeKind.Utc),
                        PositionId = 1,
                        EmployeeId = 1,
                    }
                });
        }
    }
}
