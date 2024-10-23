using MediatR;

namespace Application.CQRS.Requests.Commands.Komitent
{
    public class DeleteKomitentCommand : IRequest<bool>
    {
        public string SifraKomitenta { get; set; }

        public DeleteKomitentCommand(string sifraKomitenta)
        {
            SifraKomitenta = sifraKomitenta;
        }
    }
}
