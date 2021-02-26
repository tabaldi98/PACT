using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg.Commands;
using FluentValidation;
using System.Linq;

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
