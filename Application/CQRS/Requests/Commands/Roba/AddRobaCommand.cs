using Application.DTOs;
using MediatR;

namespace Application.CQRS.Requests.Commands.Roba
{
    public class AddRobaCommand : IRequest<RobaDTO>
    {
        public RobaDTO Roba { get; set; }

        public AddRobaCommand(RobaDTO roba)
        {
            Roba = roba;
        }
    }
}
