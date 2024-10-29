using Application.DTOs;
using MediatR;

namespace Application.CQRS.Requests.Commands.Komitent
{
    public class AddKomitentCommand : IRequest<KomitentDTO>
    {
        public KomitentDTO Komitent { get; set; }

        public AddKomitentCommand(KomitentDTO komitent)
        {
            Komitent = komitent;
        }
    }
}
