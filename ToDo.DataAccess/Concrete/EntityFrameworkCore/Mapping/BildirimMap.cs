using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Entities.Concrete;

namespace ToDo.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class BildirimMap : IEntityTypeConfiguration<Bildirim>
    {
        public void Configure(EntityTypeBuilder<Bildirim> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Aciklama).HasColumnType("ntext").IsRequired();

            builder.HasOne(x => x.AppUser).WithMany(x => x.Bildirimler).HasForeignKey(x => x.AppUserId);
        }
    }
}
