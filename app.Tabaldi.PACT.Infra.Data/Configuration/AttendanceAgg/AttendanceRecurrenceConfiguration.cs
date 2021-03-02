using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceRecurrenceAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace app.Tabaldi.PACT.Infra.Data.Configuration.AttendanceAgg
{
    public class AttendanceRecurrenceConfiguration : IEntityTypeConfiguration<AttendanceRecurrence>
    {
        public void Configure(EntityTypeBuilder<AttendanceRecurrence> builder)
        {
            builder.ToTable("AttendancesRecurrences", "dbo");
            builder.HasKey(p => p.ID);

            builder.HasOne(p => p.Client).WithMany(p => p.Recurrences).HasForeignKey(p => p.ClientID);
        }
    }
}
