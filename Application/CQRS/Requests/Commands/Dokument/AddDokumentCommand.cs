using Domain;
using MediatR;

namespace Application.CQRS.Requests.Commands.Dokument
{
    public class AddDokumentCommand : IRequest<DokumentEntity>
    {
        public DokumentEntity Dokument { get; set; }

        public AddDokumentCommand(DokumentEntity dokument)
        {
            Dokument = dokument;
        }
    }
}
