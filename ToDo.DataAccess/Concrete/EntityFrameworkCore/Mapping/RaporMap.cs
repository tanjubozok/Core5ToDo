using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Entities.Concrete;

namespace ToDo.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class RaporMap : IEntityTypeConfiguration<Rapor>
    {
        public void Configure(EntityTypeBuilder<Rapor> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Tanim).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Detay).HasColumnType("ntext");

            builder
                .HasOne(x => x.Gorev)
                .WithMany(x => x.Raporlar)
                .HasForeignKey(x => x.GorevId);
        }
    }
}
