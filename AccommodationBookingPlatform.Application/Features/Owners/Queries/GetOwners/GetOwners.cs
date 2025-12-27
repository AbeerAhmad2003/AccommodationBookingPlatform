using AccommodationBookingPlatform.Application.Common.Pagination;
using AccommodationBookingPlatform.Application.Features.Owners.DTOs;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Owners.Queries.GetOwners
{
    public record GetOwnersQuery(int PageNumber = 1, int PageSize = 10)
        : IRequest<PaginatedList<OwnerDto>>;
}
