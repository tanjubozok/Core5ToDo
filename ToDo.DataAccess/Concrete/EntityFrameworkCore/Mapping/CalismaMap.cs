using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Entities.Concrete;

namespace ToDo.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class CalismaMap : IEntityTypeConfiguration<Calisma>
    {
        public void Configure(EntityTypeBuilder<Calisma> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Ad).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Aciklama).HasColumnType("ntext");
        }
    }
}
