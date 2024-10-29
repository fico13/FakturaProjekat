using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Queries.Dokument;
using Application.DTOs;
using Application.Mappers;
using MediatR;

namespace Application.CQRS.Handlers.Queries.Dokument
{
    public class GetDokumentByBrojDokumentaQueryHandler : IRequestHandler<GetDokumentByBrojDokumentaQuery, IEnumerable<DokumentDTO>>
    {
        private readonly IDokumentRepository _dokumentRepository;

        public GetDokumentByBrojDokumentaQueryHandler(IDokumentRepository dokumentRepository)
        {
            _dokumentRepository = dokumentRepository;
        }

        public async Task<IEnumerable<DokumentDTO>> Handle(GetDokumentByBrojDokumentaQuery request, CancellationToken cancellationToken)
        {
            var dokumenti = await _dokumentRepository.GetBySifraAsync(request.BrojDokumenta);
            return dokumenti.Select(d => d.ToDokumentDTO());

        }
    }
}
