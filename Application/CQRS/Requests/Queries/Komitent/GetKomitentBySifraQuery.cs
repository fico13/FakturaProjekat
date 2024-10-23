using Domain;
using MediatR;

namespace Application.CQRS.Requests.Queries.Komitent
{
    public class GetKomitentBySifraQuery : IRequest<IEnumerable<KomitentEntity>>
    {
        public string? SifraKomitenta { get; set; }

        public GetKomitentBySifraQuery(string sifraKomitenta)
        {
            SifraKomitenta = sifraKomitenta;
        }
    }
}
