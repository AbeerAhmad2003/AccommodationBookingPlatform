using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Domain.Entities;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Commands.DeleteHotel
{
    public class DeleteHotelCommandHandler
     : IRequestHandler<DeleteHotelCommand>
    {
        private readonly IHotelRepository _hotelRepository;

        public DeleteHotelCommandHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository
                .GetByIdAsync(request.Id, cancellationToken);

            if (hotel is null)
                throw new NotFoundException(nameof(Hotel), request.Id);

            if (hotel.Bookings != null && hotel.Bookings.Any())
                throw new ConflictException("Cannot delete hotel with existing bookings.");

            await _hotelRepository.DeleteAsync(hotel, cancellationToken);

            return;
        }
    }

}
