using Application.DTOs;
using MediatR;

namespace Application.CQRS.Requests.Commands.Komitent
{
    public class UpdateKomitentCommand : IRequest<KomitentDTO?>
    {
        public KomitentDTO Komitent { get; set; }

        public UpdateKomitentCommand(KomitentDTO komitent)
        {
            Komitent = komitent;
        }
    }
}
