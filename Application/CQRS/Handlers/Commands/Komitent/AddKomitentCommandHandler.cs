using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Commands.Komitent;
using Domain;
using MediatR;

namespace Application.CQRS.Handlers.Commands.Komitent
{
    public class AddKomitentCommandHandler : IRequestHandler<AddKomitentCommand, KomitentEntity>
    {
        private readonly IKomitentRepository _komitentRepository;

        public AddKomitentCommandHandler(IKomitentRepository komitentRepository)
        {
            _komitentRepository = komitentRepository;
        }
        public async Task<KomitentEntity> Handle(AddKomitentCommand request, CancellationToken cancellationToken)
        {
            return await _komitentRepository.AddAsync(request.Komitent);
        }
    }
}
