
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using _01_DataAccessLayer.Models;

namespace Carvo.Data_Access_Layer.Data.EntitiesConfigurations
{
    public class ModelConfigurations : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            //builder.HasKey(c => c.Id);

            //builder.Property(c => c.Id)
            //       .ValueGeneratedOnAdd();

            //builder.Property(c => c.Name)
            //       .IsRequired()
            //       .HasMaxLength(50);

            //builder.Property(c => c.Description)
            //       .HasMaxLength(500);
        }
    }
}
