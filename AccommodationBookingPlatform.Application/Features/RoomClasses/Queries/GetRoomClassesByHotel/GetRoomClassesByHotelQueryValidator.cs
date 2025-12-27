using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Queries.GetRoomClassesByHotel
{
    public class GetRoomClassesByHotelQueryValidator
         : AbstractValidator<GetRoomClassesByHotelQuery>
    {
        public GetRoomClassesByHotelQueryValidator()
        {
            RuleFor(x => x.HotelId)
                .NotEmpty();
        }
    }
}
