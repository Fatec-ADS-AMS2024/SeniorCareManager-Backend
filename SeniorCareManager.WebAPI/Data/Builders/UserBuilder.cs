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
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
            entity.Property(e => e.UserType).IsRequired();
            entity.Property(e => e.UserStatus).IsRequired();
            
            entity.HasIndex(e => e.Email).IsUnique();

            entity.HasData(new User
            {
                Id = 1,
                Email = "admin@123.com",
                Password = "123456",
                UserType = Objects.Enums.UserType.ADMIN,
                UserStatus = Objects.Enums.UserStatus.ACTIVE
            });
        });
    }
}