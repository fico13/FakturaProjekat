using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Commands.Komitent;
using MediatR;

namespace Application.CQRS.Handlers.Commands.Komitent
{
    public class DeleteKomitentCommandHandler : IRequestHandler<DeleteKomitentCommand, bool>
    {
        private readonly IKomitentRepository _komitentRepository;

        public DeleteKomitentCommandHandler(IKomitentRepository komitentRepository)
        {
            _komitentRepository = komitentRepository;
        }
        public async Task<bool> Handle(DeleteKomitentCommand request, CancellationToken cancellationToken)
        {
            return await _komitentRepository.DeleteAsync(request.SifraKomitenta);
        }
    }
}
