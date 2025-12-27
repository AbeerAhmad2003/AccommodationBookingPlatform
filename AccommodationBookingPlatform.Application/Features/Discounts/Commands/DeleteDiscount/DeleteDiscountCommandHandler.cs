using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Discounts.Commands.DeleteDiscount
{
    public class DeleteDiscountCommandHandler
     : IRequestHandler<DeleteDiscountCommand>
    {
        private readonly IDiscountRepository _discountRepo;

        public DeleteDiscountCommandHandler(IDiscountRepository discountRepo)
        {
            _discountRepo = discountRepo;
        }

        public async Task Handle(
            DeleteDiscountCommand request,
            CancellationToken ct)
        {
            var discount = await _discountRepo.GetByIdAsync(request.DiscountId, ct);

            if (discount is null)
                throw new NotFoundException("Discount", request.DiscountId);

            await _discountRepo.DeleteAsync(discount, ct);
        }
    }

}
