using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Objects.Enums;

namespace SeniorCareManager.WebAPI.Data.Builders
{
    public class AllergyBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Allergy>().HasKey(a => a.Id);

            modelBuilder.Entity<Allergy>().Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Allergy>().Property(a => a.Type)
                .IsRequired();

            modelBuilder.Entity<Allergy>().HasData(new List<Allergy>
            {
                new Allergy(1, "Penicilina", AllergyType.MEDICATION),
                new Allergy(2, "Amendoim", AllergyType.FOOD),
                new Allergy(3, "Pólen", AllergyType.ENVIRONMENTAL),
                new Allergy(4, "Picada de abelha", AllergyType.INSECT_STING),
                new Allergy(5, "Pelo de gato", AllergyType.ANIMAL),
                new Allergy(6, "Látex", AllergyType.LATEX),
                new Allergy(7, "Cloro", AllergyType.OTHER)
            });
        }
    }
}
