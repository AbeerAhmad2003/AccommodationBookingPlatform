using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Domain.Entities;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Amenities.Commands.DeleteAmenity
{
    public class DeleteAmenityCommandHandler
     : IRequestHandler<DeleteAmenityCommand>
    {
        private readonly IAmenityRepository _repo;

        public DeleteAmenityCommandHandler(IAmenityRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(DeleteAmenityCommand request, CancellationToken ct)
        {
            var amenity = await _repo.GetByIdAsync(request.Id, ct);

            if (amenity is null)
                throw new NotFoundException(nameof(Amenity), request.Id);

            await _repo.DeleteAsync(amenity, ct);
        }
    }

}
