using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Queries.Roba;
using Application.DTOs;
using Application.Mappers;
using MediatR;

namespace Application.CQRS.Handlers.Queries.Roba
{
    public class GetRobaBySifraQueryHandler : IRequestHandler<GetRobaBySifraQuery, IEnumerable<RobaDTO>>
    {
        private readonly IRobaRepository _robaRepository;

        public GetRobaBySifraQueryHandler(IRobaRepository robaRepository)
        {
            _robaRepository = robaRepository;
        }

        public async Task<IEnumerable<RobaDTO>> Handle(GetRobaBySifraQuery request, CancellationToken cancellationToken)
        {
            var roba = await _robaRepository.GetBySifraAsync(request.SifraRobe);
            return roba.Select(r => r.ToRobaDTO()).ToList();
        }
    }
}
