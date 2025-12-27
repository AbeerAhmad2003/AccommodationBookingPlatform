using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetHotelById
{
    public class GetHotelByIdQueryValidator : AbstractValidator<GetHotelByIdQuery>
    {
        public GetHotelByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
