using Domain;
using MediatR;

namespace Application.CQRS.Requests.Commands.Roba
{
    public class AddRobaCommand : IRequest<RobaEntity>
    {
        public RobaEntity Roba { get; set; }

        public AddRobaCommand(RobaEntity roba)
        {
            Roba = roba;
        }
    }
}
