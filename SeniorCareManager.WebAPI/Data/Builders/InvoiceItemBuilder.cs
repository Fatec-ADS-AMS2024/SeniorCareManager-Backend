using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Builders;

public static class InvoiceItemBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<InvoiceItem>();

        entity.HasKey(ii => ii.Id);

        entity.Property(ii => ii.CfopCode).HasMaxLength(10);
        entity.Property(ii => ii.CsosnCode).HasMaxLength(10);

        ConfigureDecimal(entity, ii => ii.Quantity);
        ConfigureDecimal(entity, ii => ii.UnitPrice);
        ConfigureDecimal(entity, ii => ii.UnitCost);
        ConfigureDecimal(entity, ii => ii.TotalPrice);
        ConfigureDecimal(entity, ii => ii.TransportationAllocation);
        ConfigureDecimal(entity, ii => ii.InsuranceAllocation);
        ConfigureDecimal(entity, ii => ii.OtherChargesAllocation);
        ConfigureDecimal(entity, ii => ii.DiscountAllocation);
        ConfigureDecimal(entity, ii => ii.ICMSRate);
        ConfigureDecimal(entity, ii => ii.ICMSBaseValue);
        ConfigureDecimal(entity, ii => ii.ICMSValue);
        ConfigureDecimal(entity, ii => ii.ICMSSubBaseValue);
        ConfigureDecimal(entity, ii => ii.ICMSSubValue);
        ConfigureDecimal(entity, ii => ii.IPIRate);
        ConfigureDecimal(entity, ii => ii.IPIBaseValue);
        ConfigureDecimal(entity, ii => ii.IPIValue);

        entity.HasOne(ii => ii.Invoice)
            .WithMany(i => i.Items)
            .HasForeignKey(ii => ii.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(ii => ii.Product)
            .WithMany()
            .HasForeignKey(ii => ii.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasData(GetSeedItems());
    }

    private static void ConfigureDecimal<TProperty>(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<InvoiceItem> builder, Expression<Func<InvoiceItem, TProperty>> propertyExpression)
    {
        builder.Property(propertyExpression).HasPrecision(18, 2);
    }

    private static IEnumerable<InvoiceItem> GetSeedItems()
    {
        return new List<InvoiceItem>
        {
            new InvoiceItem
            {
                Id = 1,
                InvoiceId = 1,
                ProductId = 1,
                CfopCode = "5102",
                CsosnCode = "101",
                Quantity = 100,
                UnitPrice = 10,
                UnitCost = 9,
                TotalPrice = 1000,
                TransportationAllocation = 40,
                InsuranceAllocation = 20,
                OtherChargesAllocation = 5,
                DiscountAllocation = 10,
                ICMSRate = 18,
                ICMSBaseValue = 700,
                ICMSValue = 126,
                ICMSSubBaseValue = 0,
                ICMSSubValue = 0,
                IPIRate = 5,
                IPIBaseValue = 1000,
                IPIValue = 50
            },
            new InvoiceItem
            {
                Id = 2,
                InvoiceId = 1,
                ProductId = 2,
                CfopCode = "5102",
                CsosnCode = "101",
                Quantity = 80,
                UnitPrice = 6,
                UnitCost = 5.5m,
                TotalPrice = 480,
                TransportationAllocation = 20,
                InsuranceAllocation = 10,
                OtherChargesAllocation = 5,
                DiscountAllocation = 5,
                ICMSRate = 18,
                ICMSBaseValue = 200,
                ICMSValue = 36,
                ICMSSubBaseValue = 0,
                ICMSSubValue = 0,
                IPIRate = 5,
                IPIBaseValue = 480,
                IPIValue = 24
            },
            new InvoiceItem
            {
                Id = 3,
                InvoiceId = 2,
                ProductId = 3,
                CfopCode = "6108",
                CsosnCode = "500",
                Quantity = 50,
                UnitPrice = 20,
                UnitCost = 18,
                TotalPrice = 1000,
                TransportationAllocation = 30,
                InsuranceAllocation = 15,
                OtherChargesAllocation = 10,
                DiscountAllocation = 0,
                ICMSRate = 12,
                ICMSBaseValue = 600,
                ICMSValue = 72,
                ICMSSubBaseValue = 0,
                ICMSSubValue = 0,
                IPIRate = 0,
                IPIBaseValue = 0,
                IPIValue = 0
            },
            new InvoiceItem
            {
                Id = 4,
                InvoiceId = 3,
                ProductId = 1,
                CfopCode = "5102",
                CsosnCode = "102",
                Quantity = 150,
                UnitPrice = 12,
                UnitCost = 10,
                TotalPrice = 1800,
                TransportationAllocation = 60,
                InsuranceAllocation = 25,
                OtherChargesAllocation = 15,
                DiscountAllocation = 20,
                ICMSRate = 18,
                ICMSBaseValue = 1200,
                ICMSValue = 216,
                ICMSSubBaseValue = 0,
                ICMSSubValue = 0,
                IPIRate = 5,
                IPIBaseValue = 1800,
                IPIValue = 90
            }
        };
    }
}
