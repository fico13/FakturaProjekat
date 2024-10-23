using Domain;
using MediatR;

namespace Application.CQRS.Requests.Queries.Dokument
{
    public class GetDokumentListQuery : IRequest<IEnumerable<DokumentEntity>>
    {

    }
}
