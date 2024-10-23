using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Queries.Roba;
using Domain;
using MediatR;

namespace Application.CQRS.Handlers.Queries.Roba
{
    public class GetRobaBySifraQueryHandler : IRequestHandler<GetRobaBySifraQuery, IEnumerable<RobaEntity>>
    {
        private readonly IRobaRepository _robaRepository;

        public GetRobaBySifraQueryHandler(IRobaRepository robaRepository)
        {
            _robaRepository = robaRepository;
        }
        public async Task<IEnumerable<RobaEntity>> Handle(GetRobaBySifraQuery request, CancellationToken cancellationToken)
        {
            return await _robaRepository.GetBySifraAsync(request.SifraRobe);
        }
    }
}
