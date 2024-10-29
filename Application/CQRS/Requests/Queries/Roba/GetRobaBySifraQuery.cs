using Application.DTOs;
using MediatR;

namespace Application.CQRS.Requests.Queries.Roba
{
    public class GetRobaBySifraQuery : IRequest<IEnumerable<RobaDTO>>
    {
        public string SifraRobe { get; set; }

        public GetRobaBySifraQuery(string sifraRobe)
        {
            SifraRobe = sifraRobe;
        }
    }
}
