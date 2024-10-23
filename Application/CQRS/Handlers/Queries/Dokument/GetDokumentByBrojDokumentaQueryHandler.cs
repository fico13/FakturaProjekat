using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Queries.Dokument;
using Domain;
using MediatR;

namespace Application.CQRS.Handlers.Queries.Dokument
{
    public class GetDokumentByBrojDokumentaQueryHandler : IRequestHandler<GetDokumentByBrojDokumentaQuery, IEnumerable<DokumentEntity>>
    {
        private readonly IDokumentRepository _dokumentRepository;

        public GetDokumentByBrojDokumentaQueryHandler(IDokumentRepository dokumentRepository)
        {
            _dokumentRepository = dokumentRepository;
        }
        public Task<IEnumerable<DokumentEntity>> Handle(GetDokumentByBrojDokumentaQuery request, CancellationToken cancellationToken)
        {
            return _dokumentRepository.GetBySifraAsync(request.BrojDokumenta);
        }
    }
}
