using app.Tabaldi.PACT.LibraryModels.ClientsModule.Commands;
using FluentValidation;

namespace app.Tabaldi.PACT.Application.ClientsModule.Commands
{
    public class ClientAddCommandValidator : AbstractValidator<ClientAddCommand>
    {
        public ClientAddCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .Length(1, 255);

            RuleFor(p => p.ClinicalDiagnosis)
                .NotNull()
                .Length(1, 5000);

            RuleFor(p => p.DateOfBirth)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.Phone)
                .NotNull()
                .Length(1, 255);
        }
    }
}
