using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Application.Features.Owners.DTOs;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Owners.Queries.GetOwnerById
{
    public class GetOwnerByIdQueryHandler
        : IRequestHandler<GetOwnerByIdQuery, OwnerDto>
    {
        private readonly IOwnerRepository _repo;
        private readonly IMapper _mapper;

        public GetOwnerByIdQueryHandler(IOwnerRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OwnerDto> Handle(GetOwnerByIdQuery request, CancellationToken ct)
        {
            var owner = await _repo.GetByIdAsync(request.Id, ct);

            if (owner is null)
                throw new NotFoundException(nameof(Owner), request.Id);

            return _mapper.Map<OwnerDto>(owner);
        }
    }
}
