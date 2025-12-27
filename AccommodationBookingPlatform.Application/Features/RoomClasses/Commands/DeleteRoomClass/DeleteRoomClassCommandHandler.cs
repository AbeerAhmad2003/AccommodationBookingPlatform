using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Domain.Entities;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.DeleteRoomClass
{
    public class DeleteRoomClassCommandHandler
          : IRequestHandler<DeleteRoomClassCommand>
    {
        private readonly IRoomClassRepository _roomClassRepository;

        public DeleteRoomClassCommandHandler(IRoomClassRepository roomClassRepository)
        {
            _roomClassRepository = roomClassRepository;
        }

        public async Task Handle(
            DeleteRoomClassCommand request,
            CancellationToken cancellationToken)
        {
            var roomClass = await _roomClassRepository
                .GetByIdAsync(request.Id, cancellationToken);

            if (roomClass is null)
                throw new NotFoundException(nameof(RoomClass), request.Id);

            if (roomClass.Rooms != null && roomClass.Rooms.Any())
                throw new ConflictException("Cannot delete room class that has rooms.");

            await _roomClassRepository.DeleteAsync(roomClass, cancellationToken);
        }
    }
}
