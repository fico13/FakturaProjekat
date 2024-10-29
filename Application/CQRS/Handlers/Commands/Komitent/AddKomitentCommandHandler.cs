using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Commands.Komitent;
using Application.DTOs;
using Application.Mappers;
using MediatR;

namespace Application.CQRS.Handlers.Commands.Komitent
{
    public class AddKomitentCommandHandler : IRequestHandler<AddKomitentCommand, KomitentDTO>
    {
        private readonly IKomitentRepository _komitentRepository;

        public AddKomitentCommandHandler(IKomitentRepository komitentRepository)
        {
            _komitentRepository = komitentRepository;
        }

        public async Task<KomitentDTO> Handle(AddKomitentCommand request, CancellationToken cancellationToken)
        {
            var komitent = await _komitentRepository.AddAsync(request.Komitent.ToKomitentEntity());
            return komitent.ToKomitentDTO();
        }
    }
}
