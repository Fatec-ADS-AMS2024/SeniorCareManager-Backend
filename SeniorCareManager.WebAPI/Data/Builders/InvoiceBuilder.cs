using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Objects.Enums;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Builders;

public static class InvoiceBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Invoice>();

        entity.HasKey(i => i.Id);

        entity.Property(i => i.InvoiceNumber)
            .IsRequired()
            .HasMaxLength(20);

        entity.Property(i => i.InvoiceSeries)
            .IsRequired()
            .HasMaxLength(5);

        entity.Property(i => i.AccessKeyCode)
            .IsRequired()
            .HasMaxLength(44);

        entity.Property(i => i.PdfDanfe)
            .HasMaxLength(500);

        entity.Property(i => i.Status)
            .HasConversion<int>();

        ConfigureDecimal(entity, i => i.DiscountValue);
        ConfigureDecimal(entity, i => i.FreightValue);
        ConfigureDecimal(entity, i => i.TotalProductAmount);
        ConfigureDecimal(entity, i => i.TotalServiceAmount);
        ConfigureDecimal(entity, i => i.TotalInvoiceAmount);
        ConfigureDecimal(entity, i => i.InsuranceValue);
        ConfigureDecimal(entity, i => i.IPIValue);
        ConfigureDecimal(entity, i => i.ICMSBaseValue);
        ConfigureDecimal(entity, i => i.ICMSValue);
        ConfigureDecimal(entity, i => i.ICMSSubBaseValue);
        ConfigureDecimal(entity, i => i.ICMSSubValue);
        ConfigureDecimal(entity, i => i.FCPSTValue);
        ConfigureDecimal(entity, i => i.ISSQNBaseValue);
        ConfigureDecimal(entity, i => i.ISSQNValue);
        ConfigureDecimal(entity, i => i.OtherChargesValue);

        entity.HasIndex(i => new { i.InvoiceNumber, i.InvoiceSeries, i.SupplierId }).IsUnique();
        entity.HasIndex(i => i.AccessKeyCode).IsUnique();

        entity.HasOne(i => i.Supplier)
            .WithMany()
            .HasForeignKey(i => i.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(i => i.Carrier)
            .WithMany()
            .HasForeignKey(i => i.CarrierId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasMany(i => i.Items)
            .WithOne(ii => ii.Invoice!)
            .HasForeignKey(ii => ii.InvoiceId);

        entity.HasData(GetSeedInvoices());
    }

    private static void ConfigureDecimal<TProperty>(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Invoice> builder, Expression<Func<Invoice, TProperty>> propertyExpression)
    {
        builder.Property(propertyExpression).HasPrecision(18, 2);
    }

    private static IEnumerable<Invoice> GetSeedInvoices()
    {
        var baseDate = new DateTime(2024, 12, 1, 12, 0, 0, DateTimeKind.Utc);

        return new List<Invoice>
        {
            new Invoice
            {
                Id = 1,
                SupplierId = 1,
                CarrierId = 1,
                InvoiceNumber = "NF-2024-001",
                InvoiceSeries = "A1",
                AccessKeyCode = "12345678901234567890123456789012345678901234",
                InvoiceDate = baseDate,
                DepartureDate = baseDate.AddHours(4),
                PdfDanfe = "https://storage.local/danfes/nf-2024-001.pdf",
                Status = StatusInvoice.OPEN,
                DiscountValue = 50,
                FreightValue = 120,
                TotalProductAmount = 1500,
                TotalServiceAmount = 0,
                TotalInvoiceAmount = 1600,
                InsuranceValue = 20,
                IPIValue = 10,
                ICMSBaseValue = 900,
                ICMSValue = 162,
                ICMSSubBaseValue = 0,
                ICMSSubValue = 0,
                FCPSTValue = 0,
                ISSQNBaseValue = 0,
                ISSQNValue = 0,
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            },
            new Invoice
            {
                Id = 2,
                SupplierId = 2,
                CarrierId = 2,
                InvoiceNumber = "NF-2024-045",
                InvoiceSeries = "B1",
                AccessKeyCode = "22345678901234567890123456789012345678901234",
                InvoiceDate = baseDate.AddDays(5),
                DepartureDate = baseDate.AddDays(5).AddHours(6),
                PdfDanfe = "https://storage.local/danfes/nf-2024-045.pdf",
                Status = StatusInvoice.AUTHORIZED,
                DiscountValue = 0,
                FreightValue = 80,
                TotalProductAmount = 2150,
                TotalServiceAmount = 200,
                TotalInvoiceAmount = 2490,
                InsuranceValue = 40,
                IPIValue = 30,
                ICMSBaseValue = 1400,
                ICMSValue = 252,
                ICMSSubBaseValue = 0,
                ICMSSubValue = 0,
                FCPSTValue = 0,
                ISSQNBaseValue = 200,
                ISSQNValue = 10,
                CreatedAt = baseDate.AddDays(5),
                UpdatedAt = baseDate.AddDays(5)
            },
            new Invoice
            {
                Id = 3,
                SupplierId = 3,
                CarrierId = 1,
                InvoiceNumber = "NF-2024-090",
                InvoiceSeries = "C1",
                AccessKeyCode = "32345678901234567890123456789012345678901234",
                InvoiceDate = baseDate.AddDays(10),
                DepartureDate = baseDate.AddDays(10).AddHours(3),
                PdfDanfe = "https://storage.local/danfes/nf-2024-090.pdf",
                Status = StatusInvoice.PAID,
                DiscountValue = 100,
                FreightValue = 150,
                TotalProductAmount = 3200,
                TotalServiceAmount = 0,
                TotalInvoiceAmount = 3345,
                InsuranceValue = 60,
                IPIValue = 45,
                ICMSBaseValue = 2100,
                ICMSValue = 378,
                ICMSSubBaseValue = 0,
                ICMSSubValue = 0,
                FCPSTValue = 0,
                ISSQNBaseValue = 0,
                OtherChargesValue = 35,
                CreatedAt = baseDate.AddDays(10),
                UpdatedAt = baseDate.AddDays(10)
            }
        };
    }
}
