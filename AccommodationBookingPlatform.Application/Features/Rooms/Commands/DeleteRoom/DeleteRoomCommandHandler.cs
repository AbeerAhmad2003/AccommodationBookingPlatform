using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Domain.Entities;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Rooms.Commands.DeleteRoom
{
    public class DeleteRoomCommandHandler
      : IRequestHandler<DeleteRoomCommand>
    {
        private readonly IRoomRepository _roomRepository;

        public DeleteRoomCommandHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository
                .GetByIdAsync(request.Id, cancellationToken);

            if (room is null)
                throw new NotFoundException(nameof(Room), request.Id);

            if (room.BookingRooms.Any())
                throw new ConflictException("Cannot delete a room that has bookings.");

            await _roomRepository.DeleteAsync(room, cancellationToken);
        }
    }

}
