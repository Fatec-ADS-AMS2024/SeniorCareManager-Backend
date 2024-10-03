using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Objects.Models;
using System.Collections.Generic;

namespace SeniorCareManager.WebAPI.Data.Builders
{
    public class ProductTypeBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            // Configurando a chave primária
            modelBuilder.Entity<ProductType>().HasKey(p => p.Id);

            // Configurando a propriedade Name
            modelBuilder.Entity<ProductType>()
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();

            // Configurando o relacionamento com ProductGroup
            modelBuilder.Entity<ProductType>()
                .HasOne(d => d.ProductGroup)
                .WithMany(p => p.ProductTypes)
                .HasForeignKey(p => p.ProductGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seeding de dados
            modelBuilder.Entity<ProductType>().HasData(new List<ProductType>
            {
                new ProductType(1, "Grãos", 3),      // Certifique-se de que ProductGroupId 3 existe
                new ProductType(2, "Enlatados", 3),
                new ProductType(3, "Folhas", 3),
                new ProductType(4, "Legumes", 3),
                new ProductType(5, "Ovos", 3)
            });
        }
    }
}
