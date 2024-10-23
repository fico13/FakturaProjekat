using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Commands.Dokument;
using Domain;
using MediatR;

namespace Application.CQRS.Handlers.Commands.Dokument
{
    public class AddDokumentCommandHandler : IRequestHandler<AddDokumentCommand, DokumentEntity>
    {
        private readonly IDokumentRepository _dokumentRepository;

        public AddDokumentCommandHandler(IDokumentRepository dokumentRepository)
        {
            _dokumentRepository = dokumentRepository;
        }

        public async Task<DokumentEntity> Handle(AddDokumentCommand request, CancellationToken cancellationToken)
        {
            return await _dokumentRepository.AddAsync(request.Dokument);
        }
    }
}
