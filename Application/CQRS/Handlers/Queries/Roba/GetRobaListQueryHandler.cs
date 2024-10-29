using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Queries.Roba;
using Application.DTOs;
using Application.Mappers;
using MediatR;

namespace Application.CQRS.Handlers.Queries.Roba
{
    public class GetRobaListQueryHandler : IRequestHandler<GetRobaListQuery, IEnumerable<RobaDTO>>
    {
        private readonly IRobaRepository _robaRepository;

        public GetRobaListQueryHandler(IRobaRepository robaRepository)
        {
            _robaRepository = robaRepository;
        }

        public async Task<IEnumerable<RobaDTO>> Handle(GetRobaListQuery request, CancellationToken cancellationToken)
        {
            var roba = await _robaRepository.GetAllAsync();
            return roba.Select(r => r.ToRobaDTO()).ToList();
        }
    }
}
