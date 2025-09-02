using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Builders;

public static class UserBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(e => e.Email).HasColumnName("email").IsRequired().HasMaxLength(255);
            entity.Property(e => e.Password).HasColumnName("password").IsRequired().HasMaxLength(255);
            entity.Property(e => e.UserType).HasColumnName("usertype").IsRequired();
            entity.Property(e => e.UserStatus).HasColumnName("userstatus").IsRequired();
            
            entity.HasIndex(e => e.Email).IsUnique();
        });
    }
}