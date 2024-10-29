using Application.DTOs;
using MediatR;

namespace Application.CQRS.Requests.Commands.Dokument
{
    public class AddDokumentCommand : IRequest<DokumentDTO>
    {
        public DokumentDTO Dokument { get; set; }

        public AddDokumentCommand(DokumentDTO dokument)
        {
            Dokument = dokument;
        }
    }
}
