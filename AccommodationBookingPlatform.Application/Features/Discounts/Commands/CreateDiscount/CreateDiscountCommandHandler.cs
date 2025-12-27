using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Domain.Entities;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Discounts.Commands.CreateDiscount
{
    public class CreateDiscountCommandHandler
     : IRequestHandler<CreateDiscountCommand, Guid>
    {
        private readonly IRoomClassRepository _roomClassRepo;
        private readonly IDiscountRepository _discountRepo;

        public CreateDiscountCommandHandler(
            IRoomClassRepository roomClassRepo,
            IDiscountRepository discountRepo)
        {
            _roomClassRepo = roomClassRepo;
            _discountRepo = discountRepo;
        }

        public async Task<Guid> Handle(
            CreateDiscountCommand request,
            CancellationToken ct)
        {
            var roomClass = await _roomClassRepo.GetByIdAsync(request.RoomClassId, ct);

            if (roomClass is null)
                throw new NotFoundException("RoomClass", request.RoomClassId);

            var discount = new Discount
            {
                RoomClassId = request.RoomClassId,
                Percentage = request.Percentage,
                StartDateUtc = request.StartDateUtc,
                EndDateUtc = request.EndDateUtc,
                CreatedAtUtc = DateTime.UtcNow
            };

            await _discountRepo.AddAsync(discount, ct);

            return discount.Id;
        }
    }

}
