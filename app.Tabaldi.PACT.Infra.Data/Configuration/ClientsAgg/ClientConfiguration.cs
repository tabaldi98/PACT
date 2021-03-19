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

            builder.HasOne(p => p.User).WithMany(p => p.Clients).HasForeignKey(p => p.UserID);
        }
    }
}
