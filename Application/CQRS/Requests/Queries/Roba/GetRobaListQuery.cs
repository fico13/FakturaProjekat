using Application.DTOs;
using MediatR;

namespace Application.CQRS.Requests.Queries.Roba
{
    public class GetRobaListQuery : IRequest<IEnumerable<RobaDTO>>
    {
    }
}
