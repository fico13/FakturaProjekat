using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Queries.Komitent;
using Domain;
using MediatR;

namespace Application.CQRS.Handlers.Queries.Komitent
{
    public class GetKomitentsListQueryHandler : IRequestHandler<GetKomitentsListQuery, IEnumerable<KomitentEntity>>
    {
        private readonly IKomitentRepository _komitentRepository;

        public GetKomitentsListQueryHandler(IKomitentRepository komitentRepository)
        {
            _komitentRepository = komitentRepository;
        }

        public async Task<IEnumerable<KomitentEntity>> Handle(GetKomitentsListQuery request, CancellationToken cancellationToken)
        {
            return await _komitentRepository.GetAllAsync();
        }
    }
}
