using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Queries.Komitent;
using Domain;
using MediatR;

namespace Application.CQRS.Handlers.Queries.Komitent
{
    public class GetKomitentBySifraQueryHandler : IRequestHandler<GetKomitentBySifraQuery, IEnumerable<KomitentEntity>>
    {
        private readonly IKomitentRepository _komitentRepository;

        public GetKomitentBySifraQueryHandler(IKomitentRepository komitentRepository)
        {
            _komitentRepository = komitentRepository;
        }

        public async Task<IEnumerable<KomitentEntity>> Handle(GetKomitentBySifraQuery request, CancellationToken cancellationToken)
        {
            return await _komitentRepository.GetBySifraAsync(request.SifraKomitenta!);
        }
    }
}
