using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlassLewis.Company.Infrastructure.Data.Mappings
{
    public class CompanyMapping : IEntityTypeConfiguration<Domain.Entities.Company>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Company> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.StockTicker)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(c => c.Exchange)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.ISIN)
                .IsRequired()
                .HasColumnType("varchar(12)");

            builder.Property(c => c.Website)
                .HasColumnType("varchar(100)");

            builder.ToTable("Companies");
        }
    }
}
