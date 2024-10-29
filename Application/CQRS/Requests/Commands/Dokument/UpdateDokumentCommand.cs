using Application.DTOs;
using MediatR;

namespace Application.CQRS.Requests.Commands.Dokument
{
    public class UpdateDokumentCommand : IRequest<DokumentDTO?>
    {
        public DokumentDTO Dokument { get; set; }
        public UpdateDokumentCommand(DokumentDTO dokument)
        {
            Dokument = dokument;
        }
    }
}
