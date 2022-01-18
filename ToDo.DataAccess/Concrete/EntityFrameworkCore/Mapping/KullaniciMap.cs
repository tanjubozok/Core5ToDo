using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Entities.Concrete;

namespace ToDo.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class KullaniciMap : IEntityTypeConfiguration<Kullanici>
    {
        public void Configure(EntityTypeBuilder<Kullanici> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Ad).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Soyad).HasMaxLength(100).IsRequired(false);
            builder.Property(x => x.Telefon).HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired();

            builder.HasMany(x => x.Calismalar).WithOne(x => x.Kullanici).HasForeignKey(x => x.KullaniciId);
        }
    }
}
