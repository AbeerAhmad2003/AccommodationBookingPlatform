using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Application.Features.Owners.DTOs;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Owners.Commands.CreateOwner
{
    public class CreateOwnerCommandHandler
    : IRequestHandler<CreateOwnerCommand, OwnerDto>
    {
        private readonly IOwnerRepository _repo;
        private readonly IMapper _mapper;

        public CreateOwnerCommandHandler(IOwnerRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OwnerDto> Handle(CreateOwnerCommand request, CancellationToken ct)
        {
            var validator = new CreateOwnerValidator();
            var validationResult = await validator.ValidateAsync(request, ct);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var owner = new Owner
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            await _repo.AddAsync(owner, ct);

            return _mapper.Map<OwnerDto>(owner);
        }
    }

}
