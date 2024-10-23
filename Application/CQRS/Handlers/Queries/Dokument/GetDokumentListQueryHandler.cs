using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Queries.Dokument;
using Domain;
using MediatR;

namespace Application.CQRS.Handlers.Queries.Dokument
{
    public class GetDokumentListQueryHandler : IRequestHandler<GetDokumentListQuery, IEnumerable<DokumentEntity>>
    {
        private readonly IDokumentRepository _dokumentRepository;

        public GetDokumentListQueryHandler(IDokumentRepository dokumentRepository)
        {
            _dokumentRepository = dokumentRepository;
        }
        public async Task<IEnumerable<DokumentEntity>> Handle(GetDokumentListQuery request, CancellationToken cancellationToken)
        {
            return await _dokumentRepository.GetAllAsync();
        }
    }
}
