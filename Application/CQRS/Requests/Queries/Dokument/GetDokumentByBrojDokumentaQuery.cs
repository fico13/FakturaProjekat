using Domain;
using MediatR;

namespace Application.CQRS.Requests.Queries.Dokument
{
    public class GetDokumentByBrojDokumentaQuery : IRequest<IEnumerable<DokumentEntity>>
    {
        public string BrojDokumenta { get; set; }

        public GetDokumentByBrojDokumentaQuery(string brojDokumenta)
        {
            BrojDokumenta = brojDokumenta;
        }
    }
}
