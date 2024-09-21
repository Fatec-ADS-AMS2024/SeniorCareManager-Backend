using Microsoft.EntityFrameworkCore;

namespace SeniorCareManager.WebAPI.Data;

public class AppDbContext: DbContext
{
    //recebe a conexão do Startup
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    //public DbSet<Aluno> Alunos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Chamando Builder para configurar as entidades
        //AlunoBuilder.Build(modelBuilder);
        
    }
}