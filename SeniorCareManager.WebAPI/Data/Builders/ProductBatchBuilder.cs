using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Objects.Models;
using System;
using System.Collections.Generic;

namespace SeniorCareManager.WebAPI.Data.Builders
{
    public class ProductBatchBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductBatch>().HasKey(pb => pb.Id);
            modelBuilder.Entity<ProductBatch>().Property(pb => pb.BatchNumber)
                .IsRequired()
                .HasMaxLength(60);
            modelBuilder.Entity<ProductBatch>().Property(pb => pb.ExpirationDate)
                .IsRequired();
            modelBuilder.Entity<ProductBatch>().Property(pb => pb.CurrentQuantity)
                .IsRequired()
                .HasPrecision(10, 2);
            modelBuilder.Entity<ProductBatch>().Property(pb => pb.CurrentStockValue)
                .IsRequired()
                .HasPrecision(10, 2);
            modelBuilder.Entity<ProductBatch>()
                .HasOne(pb => pb.Product)
                .WithMany(p => p.ProductBatches)
                .HasForeignKey(pb => pb.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProductBatch>().HasData(new List<ProductBatch>
            {
                new ProductBatch(1, "L001-A", DateTime.SpecifyKind(DateTime.Parse("2026-12-31"), DateTimeKind.Utc), 100.00m, 1500.00m, 1),
                new ProductBatch(2, "L001-B", DateTime.SpecifyKind(DateTime.Parse("2027-06-30"), DateTimeKind.Utc), 50.00m, 750.00m, 1),
                new ProductBatch(3, "XPT-05", DateTime.SpecifyKind(DateTime.Parse("2025-10-20"), DateTimeKind.Utc), 200.00m, 500.00m, 2)
            });
        }
    }
}