using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Commands.Dokument;
using MediatR;

namespace Application.CQRS.Handlers.Commands.Dokument
{
    public class DeleteDokumentCommandHandler : IRequestHandler<DeleteDokumentCommand, bool>
    {
        private readonly IDokumentRepository _dokumentRepository;

        public DeleteDokumentCommandHandler(IDokumentRepository dokumentRepository)
        {
            _dokumentRepository = dokumentRepository;
        }
        public async Task<bool> Handle(DeleteDokumentCommand request, CancellationToken cancellationToken)
        {
            return await _dokumentRepository.DeleteAsync(request.BrojDokumenta);
        }
    }
}
