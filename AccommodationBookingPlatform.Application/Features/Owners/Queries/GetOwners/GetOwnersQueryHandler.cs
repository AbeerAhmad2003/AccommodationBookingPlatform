using AccommodationBookingPlatform.Application.Common.Pagination;
using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Features.Owners.DTOs;
using AccommodationBookingPlatform.Domain.Common;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Owners.Queries.GetOwners
{
    public class GetOwnersQueryHandler
        : IRequestHandler<GetOwnersQuery, PaginatedList<OwnerDto>>
    {
        private readonly IOwnerRepository _repo;
        private readonly IMapper _mapper;

        public GetOwnersQueryHandler(IOwnerRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PaginatedList<OwnerDto>> Handle(GetOwnersQuery request, CancellationToken ct)
        {
            var query = new Query<Owner>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            var result = await _repo.GetOwnersAsync(query, ct);

            return new PaginatedList<OwnerDto>(
                _mapper.Map<List<OwnerDto>>(result.Items),
                result.TotalCount,
                result.PageNumber,
                result.PageSize
            );
        }
    }
}
