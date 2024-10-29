using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Queries.Komitent;
using Application.DTOs;
using Application.Mappers;
using MediatR;

namespace Application.CQRS.Handlers.Queries.Komitent
{
    public class GetKomitentBySifraQueryHandler : IRequestHandler<GetKomitentBySifraQuery, IEnumerable<KomitentDTO>>
    {
        private readonly IKomitentRepository _komitentRepository;

        public GetKomitentBySifraQueryHandler(IKomitentRepository komitentRepository)
        {
            _komitentRepository = komitentRepository;
        }

        public async Task<IEnumerable<KomitentDTO>> Handle(GetKomitentBySifraQuery request, CancellationToken cancellationToken)
        {
            var komitenti = await _komitentRepository.GetBySifraAsync(request.SifraKomitenta!);
            return komitenti.Select(k => k.ToKomitentDTO()).ToList();
        }
    }
}
