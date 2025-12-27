using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.AssignAmenityToRoomClass
{
    public class AssignAmenityToRoomClassHandler
    : IRequestHandler<AssignAmenityToRoomClassCommand>
    {
        private readonly IRoomClassRepository _roomRepo;
        private readonly IAmenityRepository _amenityRepo;

        public AssignAmenityToRoomClassHandler(
            IRoomClassRepository roomRepo,
            IAmenityRepository amenityRepo)
        {
            _roomRepo = roomRepo;
            _amenityRepo = amenityRepo;
        }

        public async Task Handle(
            AssignAmenityToRoomClassCommand request,
            CancellationToken ct)
        {
            var roomClass = await _roomRepo.GetByIdWithDetailsAsync(request.RoomClassId, ct);
            if (roomClass is null)
                throw new NotFoundException("RoomClass", request.RoomClassId);

            var amenity = await _amenityRepo.GetByIdAsync(request.AmenityId, ct);
            if (amenity is null)
                throw new NotFoundException("Amenity", request.AmenityId);

            if (roomClass.Amenities.Any(a => a.Id == amenity.Id))
                return; // already assigned

            roomClass.Amenities.Add(amenity);

            await _roomRepo.UpdateAsync(roomClass, ct);
        }
    }

}
