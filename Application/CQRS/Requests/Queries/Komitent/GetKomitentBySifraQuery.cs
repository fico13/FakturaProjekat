using Application.DTOs;
using MediatR;

namespace Application.CQRS.Requests.Queries.Komitent
{
    public class GetKomitentBySifraQuery : IRequest<IEnumerable<KomitentDTO>>
    {
        public string? SifraKomitenta { get; set; }

        public GetKomitentBySifraQuery(string sifraKomitenta)
        {
            SifraKomitenta = sifraKomitenta;
        }
    }
}
