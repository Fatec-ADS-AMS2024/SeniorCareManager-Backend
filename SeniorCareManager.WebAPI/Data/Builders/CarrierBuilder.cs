using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Objects.Models;


namespace SeniorCareManager.WebAPI.Data.Builders
{
    public class CarrierBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarrierBuilder>().HasKey(pg => pg.Id);
            modelBuilder.Entity<CarrierBuilder>().HasKey(pg => pg.Id);

        }
    }
}
