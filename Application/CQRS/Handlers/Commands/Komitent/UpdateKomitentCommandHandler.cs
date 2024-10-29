using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Commands.Komitent;
using Application.DTOs;
using Application.Mappers;
using MediatR;

namespace Application.CQRS.Handlers.Commands.Komitent
{
    public class UpdateKomitentCommandHandler : IRequestHandler<UpdateKomitentCommand, KomitentDTO?>
    {
        private readonly IKomitentRepository _komitentRepository;

        public UpdateKomitentCommandHandler(IKomitentRepository komitentRepository)
        {
            _komitentRepository = komitentRepository;
        }

        public async Task<KomitentDTO?> Handle(UpdateKomitentCommand request, CancellationToken cancellationToken)
        {
            var komitent = await _komitentRepository.UpdateAsync(request.Komitent.ToKomitentEntity());
            if (komitent == null)
            {
                return null;
            }
            return komitent.ToKomitentDTO();
        }
    }
}
