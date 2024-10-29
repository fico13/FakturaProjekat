using Application.DTOs;
using MediatR;

namespace Application.CQRS.Requests.Queries.Dokument
{
    public class GetDokumentListQuery : IRequest<IEnumerable<DokumentDTO>>
    {

    }
}
