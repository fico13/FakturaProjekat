using Domain;
using MediatR;

namespace Application.CQRS.Requests.Commands.Komitent
{
    public class AddKomitentCommand : IRequest<KomitentEntity>
    {
        public KomitentEntity Komitent { get; set; }

        public AddKomitentCommand(KomitentEntity komitent)
        {
            Komitent = komitent;
        }
    }
}
