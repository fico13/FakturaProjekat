using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Commands.Roba;
using Application.DTOs;
using Application.Mappers;
using MediatR;

namespace Application.CQRS.Handlers.Commands.Roba
{
    public class AddRobaCommandHandler : IRequestHandler<AddRobaCommand, RobaDTO>
    {
        private readonly IRobaRepository _robaRepository;

        public AddRobaCommandHandler(IRobaRepository robaRepository)
        {
            _robaRepository = robaRepository;
        }

        public async Task<RobaDTO> Handle(AddRobaCommand request, CancellationToken cancellationToken)
        {
            var roba = await _robaRepository.AddAsync(request.Roba.ToRobaEntity());
            return roba.ToRobaDTO();
        }
    }
}
