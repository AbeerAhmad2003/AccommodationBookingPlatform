using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Queries.GetRoomClassById
{
    public class GetRoomClassByIdQueryValidator
         : AbstractValidator<GetRoomClassByIdQuery>
    {
        public GetRoomClassByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
