using MediatR;

namespace Application.CQRS.Requests.Commands.Roba
{
    public class DeleteRobaCommand : IRequest<bool>
    {
        public string SifraRobe { get; set; }

        public DeleteRobaCommand(string sifraRobe)
        {
            SifraRobe = sifraRobe;
        }
    }
}
