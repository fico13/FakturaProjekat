using Domain;
using MediatR;

namespace Application.CQRS.Requests.Queries.Roba
{
    public class GetRobaListQuery : IRequest<IEnumerable<RobaEntity>>
    {
    }
}
