using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Data.Builders;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data;

public class AppDbContext: DbContext
{
    //recebe a conexão do Startup
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    public DbSet<ProductGroup> ProductGroups { get; set; }
    public DbSet<Religion> Religions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Chamando Builder para configurar as entidades
        ProductGroupBuilder.Build(modelBuilder);
        ReligionBuilder.Build(modelBuilder);

    }
}