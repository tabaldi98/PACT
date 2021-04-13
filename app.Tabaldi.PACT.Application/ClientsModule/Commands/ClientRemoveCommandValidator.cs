using app.Tabaldi.PACT.LibraryModels.ClientsModule.Commands;
using FluentValidation;

namespace app.Tabaldi.PACT.Application.ClientsModule.Commands
{
    public class ClientRemoveCommandValidator : AbstractValidator<ClientRemoveCommand>
    {
        public ClientRemoveCommandValidator()
        {
            RuleFor(p => p.IDs)
                .NotNull();
        }
    }
}
