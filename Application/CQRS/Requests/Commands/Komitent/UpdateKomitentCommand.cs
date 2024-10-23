using Domain;
using MediatR;

namespace Application.CQRS.Requests.Commands.Komitent
{
    public class UpdateKomitentCommand : IRequest<KomitentEntity?>
    {
        public KomitentEntity Komitent { get; set; }

        public UpdateKomitentCommand(KomitentEntity komitent)
        {
            Komitent = komitent;
        }
    }
}
