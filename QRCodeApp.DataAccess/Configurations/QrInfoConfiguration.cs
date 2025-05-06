using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QRCodeApp.DataAccess.Entities;

namespace QRCodeApp.DataAccess.Configurations
{
    public class QrInfoConfiguration : IEntityTypeConfiguration<QrInfoEntity>
    {
        public void Configure(EntityTypeBuilder<QrInfoEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(q => q.CreatedAt)
                .IsRequired();

            builder.Property(q => q.Content)
                .IsRequired();
        }
    }
}
