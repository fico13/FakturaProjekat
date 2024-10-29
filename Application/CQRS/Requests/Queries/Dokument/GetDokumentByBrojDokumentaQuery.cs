using Application.DTOs;
using MediatR;

namespace Application.CQRS.Requests.Queries.Dokument
{
    public class GetDokumentByBrojDokumentaQuery : IRequest<IEnumerable<DokumentDTO>>
    {
        public string BrojDokumenta { get; set; }

        public GetDokumentByBrojDokumentaQuery(string brojDokumenta)
        {
            BrojDokumenta = brojDokumenta;
        }
    }
}
