using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Queries.Roba;
using Domain;
using MediatR;

namespace Application.CQRS.Handlers.Queries.Roba
{
    public class GetRobaListQueryHandler : IRequestHandler<GetRobaListQuery, IEnumerable<RobaEntity>>
    {
        private readonly IRobaRepository _robaRepository;

        public GetRobaListQueryHandler(IRobaRepository robaRepository)
        {
            _robaRepository = robaRepository;
        }
        public async Task<IEnumerable<RobaEntity>> Handle(GetRobaListQuery request, CancellationToken cancellationToken)
        {
            return await _robaRepository.GetAllAsync();
        }
    }
}
