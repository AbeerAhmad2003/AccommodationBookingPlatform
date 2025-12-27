using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.RemoveAmenityFromRoomClass
{
    public class RemoveAmenityFromRoomClassHandler
    : IRequestHandler<RemoveAmenityFromRoomClassCommand>
    {
        private readonly IRoomClassRepository _roomRepo;

        public RemoveAmenityFromRoomClassHandler(IRoomClassRepository roomRepo)
        {
            _roomRepo = roomRepo;
        }

        public async Task Handle(
            RemoveAmenityFromRoomClassCommand request,
            CancellationToken ct)
        {
            var roomClass = await _roomRepo.GetByIdWithDetailsAsync(request.RoomClassId, ct);

            if (roomClass is null)
                throw new NotFoundException("RoomClass", request.RoomClassId);

            var amenity = roomClass.Amenities
                .FirstOrDefault(a => a.Id == request.AmenityId);

            if (amenity is null)
                return;

            roomClass.Amenities.Remove(amenity);

            await _roomRepo.UpdateAsync(roomClass, ct);
        }
    }

}
