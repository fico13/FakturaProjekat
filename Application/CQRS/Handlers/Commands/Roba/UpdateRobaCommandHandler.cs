using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Commands.Roba;
using Application.DTOs;
using Application.Mappers;
using MediatR;

namespace Application.CQRS.Handlers.Commands.Roba
{
    public class UpdateRobaCommandHandler : IRequestHandler<UpdateRobaCommand, RobaDTO?>
    {
        private readonly IRobaRepository _robaRepository;

        public UpdateRobaCommandHandler(IRobaRepository robaRepository)
        {
            _robaRepository = robaRepository;
        }

        public async Task<RobaDTO?> Handle(UpdateRobaCommand request, CancellationToken cancellationToken)
        {
            var roba = await _robaRepository.UpdateAsync(request.Roba.ToRobaEntity());
            if (roba == null) return null;
            return roba.ToRobaDTO();
        }
    }
}
