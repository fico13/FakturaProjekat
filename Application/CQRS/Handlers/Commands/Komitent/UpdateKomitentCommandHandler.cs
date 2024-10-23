using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Commands.Komitent;
using Domain;
using MediatR;

namespace Application.CQRS.Handlers.Commands.Komitent
{
    public class UpdateKomitentCommandHandler : IRequestHandler<UpdateKomitentCommand, KomitentEntity?>
    {
        private readonly IKomitentRepository _komitentRepository;

        public UpdateKomitentCommandHandler(IKomitentRepository komitentRepository)
        {
            _komitentRepository = komitentRepository;
        }
        public async Task<KomitentEntity?> Handle(UpdateKomitentCommand request, CancellationToken cancellationToken)
        {
            return await _komitentRepository.UpdateAsync(request.Komitent);
        }
    }
}
