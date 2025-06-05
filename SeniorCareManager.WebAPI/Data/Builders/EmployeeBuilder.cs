using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Objects.Enums;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Builders
{
    public class EmployeeBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasKey(e => e.Id);

            modelBuilder.Entity<Employee>().Property(e => e.Name).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<Employee>().Property(e => e.Cpf).IsRequired().HasMaxLength(14);

            modelBuilder.Entity<Employee>().Property(e => e.Phone).IsRequired().HasMaxLength(15);

            modelBuilder.Entity<Employee>().Property(e => e.Email).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<Employee>().Property(e => e.HireDate).IsRequired();

            modelBuilder.Entity<Employee>().Property(e => e.State).IsRequired().HasMaxLength(2);

            modelBuilder.Entity<Employee>().Property(e => e.City).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<Employee>().Property(e => e.Street).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<Employee>().Property(e => e.Cep).IsRequired().HasMaxLength(9);

            modelBuilder.Entity<Employee>().Property(e => e.Number).IsRequired();

            modelBuilder.Entity<Employee>().Property(e => e.Neighborhood).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<Employee>().Property(e => e.StatusEmployee).IsRequired();


            modelBuilder.Entity<Employee>().HasData(new List<Employee>
            {
                    new Employee(1, "João da Silva", "123.456.789-00", "(11) 91234-5678", "joao@email.com", DateTime.Now, "SP", "São Paulo", "Rua A", "01000-000", 123, "Centro", StatusEmployee.ACTIVE, 1),
                    new Employee(2, "Maria Oliveira", "987.654.321-00", "(21) 99876-5432", "maria.oliveira@email.com", DateTime.Now, "RJ", "Rio de Janeiro", "Avenida B", "20000-000", 456, "Copacabana", StatusEmployee.ACTIVE, 2),
                    new Employee(3, "Carlos Pereira", "456.789.123-00", "(31) 98765-4321", "carlos.pereira@email.com", DateTime.Now, "MG", "Belo Horizonte", "Rua C", "30000-000", 789, "Savassi", StatusEmployee.FIRED, 3)
                   });
        }
    }
}