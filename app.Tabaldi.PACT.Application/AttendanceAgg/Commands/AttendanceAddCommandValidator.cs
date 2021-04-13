using app.Tabaldi.PACT.LibraryModels.AttendanceModule.Commands;
using FluentValidation;

namespace app.Tabaldi.PACT.Application.AttendanceAgg.Commands
{
    public class AttendanceAddCommandValidator: AbstractValidator<AttendanceAddCommand>
    {
        public AttendanceAddCommandValidator()
        {
            RuleFor(p => p.ClientID)
                .NotNull()
                .GreaterThan(0);

            RuleFor(p => p.Date)
                .NotNull();

            RuleFor(p => p.HourInitial)
                .NotNull();

            RuleFor(p => p.HourFinish)
                .NotNull()
                .GreaterThan(p => p.HourInitial);
        }
    }
}
