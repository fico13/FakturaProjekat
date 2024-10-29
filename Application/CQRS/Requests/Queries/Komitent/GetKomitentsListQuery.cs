using Application.DTOs;
using MediatR;

namespace Application.CQRS.Requests.Queries.Komitent
{
    public class GetKomitentsListQuery : IRequest<IEnumerable<KomitentDTO>>
    {
    }
}
