using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Commands.Dokument;
using Application.DTOs;
using Application.Mappers;
using MediatR;

namespace Application.CQRS.Handlers.Commands.Dokument
{
    public class UpdateDokumentCommandHandler : IRequestHandler<UpdateDokumentCommand, DokumentDTO?>
    {
        private readonly IDokumentRepository _dokumentRepository;

        public UpdateDokumentCommandHandler(IDokumentRepository dokumentRepository)
        {
            _dokumentRepository = dokumentRepository;
        }

        public async Task<DokumentDTO?> Handle(UpdateDokumentCommand request, CancellationToken cancellationToken)
        {
            var dokument = await _dokumentRepository.UpdateAsync(request.Dokument.ToDokumentEntity());

            return dokument?.ToDokumentDTO();
        }
    }
}
