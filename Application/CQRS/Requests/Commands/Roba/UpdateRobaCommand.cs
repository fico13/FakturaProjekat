using Application.DTOs;
using MediatR;

namespace Application.CQRS.Requests.Commands.Roba
{
    public class UpdateRobaCommand : IRequest<RobaDTO?>
    {
        public RobaDTO Roba { get; set; }

        public UpdateRobaCommand(RobaDTO roba)
        {
            Roba = roba;
        }
    }
}
