using Domain;
using MediatR;

namespace Application.CQRS.Requests.Commands.Dokument
{
    public class UpdateDokumentCommand : IRequest<DokumentEntity?>
    {
        public DokumentEntity Dokument { get; set; }
        public UpdateDokumentCommand(DokumentEntity dokument)
        {
            Dokument = dokument;
        }
    }
}
