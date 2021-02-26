using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace app.Tabaldi.PACT.Infra.Data.Configuration.AttendanceAgg
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.ToTable("Attendances", "dbo");
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
