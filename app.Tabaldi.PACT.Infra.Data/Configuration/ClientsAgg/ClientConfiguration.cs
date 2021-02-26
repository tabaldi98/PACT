using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace app.Tabaldi.PACT.Infra.Data.Configuration.ClientsAgg
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients", "dbo");
            builder.HasKey(x => x.ID);

            //builder.Property(x => x.ID).HasIntColumnType().ValueGeneratedOnAdd().IsRequired();
            //builder.Property(x => x.Name).HasVarcharUnicodeColumnType(255).IsRequired();
            //builder.Property(x => x.Phone).HasVarcharUnicodeColumnType(255).IsRequired();
            //builder.Property(x => x.InstagramURL).HasVarcharUnicodeColumnType(255).IsOptional();
            //builder.Property(x => x.FacebookURL).HasVarcharUnicodeColumnType(255).IsOptional();
            //builder.Property(x => x.WhatsAppURL).HasVarcharUnicodeColumnType(255).IsOptional();
        }
    }
}
