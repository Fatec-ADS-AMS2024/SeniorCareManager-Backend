using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Objects.Enums;

namespace SeniorCareManager.WebAPI.Data.Builders
{
	public class ProductBuilder
	{
		public static void Build(ModelBuilder modelBuilder)
		{
			// Configura a chave primária
			modelBuilder.Entity<Product>().HasKey(p => p.Id);

			modelBuilder.Entity<Product>().Property(p => p.Description).IsRequired().HasMaxLength(100);
			modelBuilder.Entity<Product>().Property(p => p.GenericName).IsRequired().HasMaxLength(100);

			modelBuilder.Entity<Product>().Property(p => p.MinimumStock).HasColumnType("decimal(10,2)");

			modelBuilder.Entity<Product>().Property(p => p.CurrentStock).HasColumnType("decimal(10,2)");

			modelBuilder.Entity<Product>().Property(p => p.StockValue).HasColumnType("decimal(10,2)");

			modelBuilder.Entity<Product>().Property(p => p.UnitPrice).HasColumnType("decimal(10,2)");

			modelBuilder.Entity<Product>().Property(p => p.AverageCost).HasColumnType("decimal(10,2)");

			modelBuilder.Entity<Product>().Property(p => p.LastPurchasePrice).HasColumnType("decimal(10,2)");

			modelBuilder.Entity<Product>().Property(p => p.HighCost).HasConversion<int>();

			modelBuilder.Entity<Product>().Property(p => p.ExpirationControlled).HasConversion<int>();

			// Inserção de dados iniciais
			modelBuilder.Entity<Product>().HasData(new List<Product>
			{
				new Product(
					id: 1,
					description: "Paracetamol 500mg",
					genericName: "Paracetamol",
					minimumStock: 100,
					currentStock: 500,
					stockValue: 2500,
					unitPrice: 5,
					averageCost: 4.5m,
					lastPurchasePrice: 4.8m,
					highCost: YesNo.NO,
					expirationControlled: YesNo.YES
				),
				new Product(
					id: 2,
					description: "Ibuprofeno 600mg",
					genericName: "Ibuprofeno",
					minimumStock: 50,
					currentStock: 200,
					stockValue: 1200,
					unitPrice: 6,
					averageCost: 5.5m,
					lastPurchasePrice: 5.8m,
					highCost: YesNo.NO,
					expirationControlled: YesNo.YES
				),
				new Product(
					id: 3,
					description: "Amoxicilina 500mg",
					genericName: "Amoxicilina",
					minimumStock: 75,
					currentStock: 300,
					stockValue: 1800,
					unitPrice: 6,
					averageCost: 5.7m,
					lastPurchasePrice: 5.9m,
					highCost: YesNo.YES,
					expirationControlled: YesNo.YES
				)
			});
		}
	}
}
