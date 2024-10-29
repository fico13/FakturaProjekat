using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Commands.Dokument;
using Application.DTOs;
using Application.Mappers;
using MediatR;

namespace Application.CQRS.Handlers.Commands.Dokument
{
    public class AddDokumentCommandHandler : IRequestHandler<AddDokumentCommand, DokumentDTO>
    {
        private readonly IDokumentRepository _dokumentRepository;

        public AddDokumentCommandHandler(IDokumentRepository dokumentRepository)
        {
            _dokumentRepository = dokumentRepository;
        }

        public async Task<DokumentDTO> Handle(AddDokumentCommand request, CancellationToken cancellationToken)
        {
            var dokument = await _dokumentRepository.AddAsync(request.Dokument.ToDokumentEntity());
            return dokument.ToDokumentDTO();
        }
    }
}
