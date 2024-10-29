using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Queries.Komitent;
using Application.DTOs;
using Application.Mappers;
using MediatR;

namespace Application.CQRS.Handlers.Queries.Komitent
{
    public class GetKomitentsListQueryHandler : IRequestHandler<GetKomitentsListQuery, IEnumerable<KomitentDTO>>
    {
        private readonly IKomitentRepository _komitentRepository;

        public GetKomitentsListQueryHandler(IKomitentRepository komitentRepository)
        {
            _komitentRepository = komitentRepository;
        }

        public async Task<IEnumerable<KomitentDTO>> Handle(GetKomitentsListQuery request, CancellationToken cancellationToken)
        {
            var komitenti = await _komitentRepository.GetAllAsync();
            return komitenti.Select(k => k.ToKomitentDTO()).ToList();
        }
    }
}
