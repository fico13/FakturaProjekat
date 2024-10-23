using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Commands.Dokument;
using Domain;
using MediatR;

namespace Application.CQRS.Handlers.Commands.Dokument
{
    public class UpdateDokumentCommandHandler : IRequestHandler<UpdateDokumentCommand, DokumentEntity?>
    {
        private readonly IDokumentRepository _dokumentRepository;

        public UpdateDokumentCommandHandler(IDokumentRepository dokumentRepository)
        {
            _dokumentRepository = dokumentRepository;
        }
        public async Task<DokumentEntity?> Handle(UpdateDokumentCommand request, CancellationToken cancellationToken)
        {
            return await _dokumentRepository.UpdateAsync(request.Dokument);
        }
    }
}
