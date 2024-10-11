using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Data.Builders;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data
{
    //recebe a conexão do Startup
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    public DbSet<ProductGroup> ProductGroups { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<HealthInsurancePlan> HealthInsurancePlans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ProductGroupBuilder.Build(modelBuilder);
            ManufacturerBuilder.Build(modelBuilder);
            CarrierBuilder.Build(modelBuilder);
            HealthInsurancePlanBuilder.Build(modelBuilder);
        }
    }
}