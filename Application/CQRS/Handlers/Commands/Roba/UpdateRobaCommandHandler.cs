using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Commands.Roba;
using Domain;
using MediatR;

namespace Application.CQRS.Handlers.Commands.Roba
{
    public class UpdateRobaCommandHandler : IRequestHandler<UpdateRobaCommand, RobaEntity?>
    {
        private readonly IRobaRepository _robaRepository;

        public UpdateRobaCommandHandler(IRobaRepository robaRepository)
        {
            _robaRepository = robaRepository;
        }
        public async Task<RobaEntity?> Handle(UpdateRobaCommand request, CancellationToken cancellationToken)
        {
            return await _robaRepository.UpdateAsync(request.Roba);
        }
    }
}
