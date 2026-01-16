using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimplCommerce.Module.Catalog.Models;

namespace SimplCommerce.Module.Catalog.Models
{
    public class PackagingInquiryTypeConfiguration : IEntityTypeConfiguration<PackagingInquiry>
    {
        public void Configure(EntityTypeBuilder<PackagingInquiry> builder)
        {
            builder.ToTable("Catalog_PackagingInquiry");

            builder.Property(x => x.ProductSku).HasMaxLength(450);
            builder.Property(x => x.ProductName).HasMaxLength(450);
            builder.Property(x => x.CompanyName).HasMaxLength(450);
            builder.Property(x => x.ContactName).HasMaxLength(450);
            builder.Property(x => x.ContactEmail).HasMaxLength(450);

            builder.Property(x => x.BoxDimensions);
            builder.Property(x => x.QuantityNeeded);
            builder.Property(x => x.Message);
        }
    }
}
