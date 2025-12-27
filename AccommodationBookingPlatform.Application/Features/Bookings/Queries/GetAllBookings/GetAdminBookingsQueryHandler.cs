using AccommodationBookingPlatform.Application.Common.Pagination;
using AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services;
using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Domain.Common;
using AccommodationBookingPlatform.Domain.Common.Enums;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetAllBookings
{
    public class GetAdminBookingsQueryHandler
  : IRequestHandler<GetAdminBookingsQuery, PaginatedList<AdminBookingListItemDto>>
    {
        private readonly IBookingRepository _repo;
        private readonly ICurrentUserService _user;
        private readonly IMapper _mapper;

        public GetAdminBookingsQueryHandler(
            IBookingRepository repo,
            ICurrentUserService user,
            IMapper mapper)
        {
            _repo = repo;
            _user = user;
            _mapper = mapper;
        }

        public async Task<PaginatedList<AdminBookingListItemDto>> Handle(
            GetAdminBookingsQuery request,
            CancellationToken ct)
        {
            var isAdmin = _user.Role == UserRole.Admin;

            if (!isAdmin)
                throw new ForbiddenException("Admins only");

            var query = new Query<Booking>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            var result = await _repo.GetBookingsAsync(query, ct);

            var mappedItems = _mapper.Map<List<AdminBookingListItemDto>>(result.Items);

            return new PaginatedList<AdminBookingListItemDto>(
                mappedItems,
                result.TotalCount,
                result.PageNumber,
                result.PageSize);
        }
    }

}
