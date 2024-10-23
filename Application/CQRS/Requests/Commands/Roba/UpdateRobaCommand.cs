using Domain;
using MediatR;

namespace Application.CQRS.Requests.Commands.Roba
{
    public class UpdateRobaCommand : IRequest<RobaEntity?>
    {
        public RobaEntity Roba { get; set; }

        public UpdateRobaCommand(RobaEntity roba)
        {
            Roba = roba;
        }
    }
}
