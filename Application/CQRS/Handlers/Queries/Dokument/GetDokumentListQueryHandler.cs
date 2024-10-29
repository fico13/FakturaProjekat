using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Queries.Dokument;
using Application.DTOs;
using Application.Mappers;
using MediatR;

namespace Application.CQRS.Handlers.Queries.Dokument
{
    public class GetDokumentListQueryHandler : IRequestHandler<GetDokumentListQuery, IEnumerable<DokumentDTO>>
    {
        private readonly IDokumentRepository _dokumentRepository;

        public GetDokumentListQueryHandler(IDokumentRepository dokumentRepository)
        {
            _dokumentRepository = dokumentRepository;
        }

        public async Task<IEnumerable<DokumentDTO>> Handle(GetDokumentListQuery request, CancellationToken cancellationToken)
        {
            var dokumenti = await _dokumentRepository.GetAllAsync();
            return dokumenti.Select(d => d.ToDokumentDTO());
        }
    }
}
