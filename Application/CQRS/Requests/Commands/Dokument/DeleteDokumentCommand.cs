using MediatR;

namespace Application.CQRS.Requests.Commands.Dokument
{
    public class DeleteDokumentCommand : IRequest<bool>
    {
        public string BrojDokumenta { get; set; }

        public DeleteDokumentCommand(string brojDokumenta)
        {
            BrojDokumenta = brojDokumenta;
        }
    }
}
