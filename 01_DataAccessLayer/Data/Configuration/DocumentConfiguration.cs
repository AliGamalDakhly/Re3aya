
using _01_DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _01_DataAccessLayer.Data.Configuration
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasOne(d => d.Doctor)
                .WithMany(doc => doc.Documents)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(d => d.FilePath)
                .IsRequired();

        }

    }
}
