using Application.Contracts.Interfaces;
using Application.CQRS.Requests.Commands.Roba;
using MediatR;

namespace Application.CQRS.Handlers.Commands.Roba
{
    public class DeleteRobaCommandHandler : IRequestHandler<DeleteRobaCommand, bool>
    {
        private readonly IRobaRepository _robaRepository;

        public DeleteRobaCommandHandler(IRobaRepository robaRepository)
        {
            _robaRepository = robaRepository;
        }
        public async Task<bool> Handle(DeleteRobaCommand request, CancellationToken cancellationToken)
        {
            return await _robaRepository.DeleteAsync(request.SifraRobe);
        }
    }
}
