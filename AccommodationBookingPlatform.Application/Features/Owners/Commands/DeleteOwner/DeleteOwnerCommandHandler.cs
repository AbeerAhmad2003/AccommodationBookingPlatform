using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Domain.Entities;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Owners.Commands.DeleteOwner
{
    public class DeleteOwnerCommandHandler
     : IRequestHandler<DeleteOwnerCommand, bool>
    {
        private readonly IOwnerRepository _repo;

        public DeleteOwnerCommandHandler(IOwnerRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteOwnerCommand request, CancellationToken ct)
        {
            var owner = await _repo.GetByIdAsync(request.Id, ct);
            if (owner is null)
                throw new NotFoundException(nameof(Owner), request.Id);

            if (owner.Hotels.Any())
                throw new ConflictException("Owner cannot be deleted because he owns hotels.");

            await _repo.DeleteAsync(owner, ct);
            return true;
        }
    }

}
