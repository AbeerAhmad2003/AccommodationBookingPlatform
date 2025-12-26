using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.SearchHotels
{
    public class SearchHotelsQueryValidator
       : AbstractValidator<SearchHotelsQuery>
    {
        public SearchHotelsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);

            RuleFor(x => x.CheckOut)
                .GreaterThan(x => x.CheckIn)
                .When(x => x.CheckIn.HasValue && x.CheckOut.HasValue);

            RuleFor(x => x.Rooms)
                .GreaterThan(0);

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(x => x.MinPrice)
                .When(x => x.MinPrice.HasValue && x.MaxPrice.HasValue);

            RuleFor(x => x.MaxStars)
                .GreaterThanOrEqualTo(x => x.MinStars)
                .When(x => x.MinStars.HasValue && x.MaxStars.HasValue);
        }
    }
}
