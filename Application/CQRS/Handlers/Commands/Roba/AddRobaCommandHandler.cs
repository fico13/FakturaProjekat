using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Commands.Roba;
using Domain;
using MediatR;

namespace Application.CQRS.Handlers.Commands.Roba
{
    public class AddRobaCommandHandler : IRequestHandler<AddRobaCommand, RobaEntity>
    {
        private readonly IRobaRepository _robaRepository;

        public AddRobaCommandHandler(IRobaRepository robaRepository)
        {
            _robaRepository = robaRepository;
        }
        public async Task<RobaEntity> Handle(AddRobaCommand request, CancellationToken cancellationToken)
        {
            return await _robaRepository.AddAsync(request.Roba);
        }
    }
}
