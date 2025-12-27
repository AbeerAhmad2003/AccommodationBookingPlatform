using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Application.Features.Owners.DTOs;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Owners.Commands.UpdateOwner
{
    public class UpdateOwnerCommandHandler
     : IRequestHandler<UpdateOwnerCommand, OwnerDto>
    {
        private readonly IOwnerRepository _repo;
        private readonly IMapper _mapper;

        public UpdateOwnerCommandHandler(IOwnerRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OwnerDto> Handle(UpdateOwnerCommand request, CancellationToken ct)
        {
            var validator = new UpdateOwnerValidator();
            var validationResult = await validator.ValidateAsync(request, ct);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var owner = await _repo.GetByIdAsync(request.Id, ct);

            if (owner is null)
                throw new NotFoundException(nameof(Owner), request.Id);

            if (!string.IsNullOrWhiteSpace(request.FirstName))
                owner.FirstName = request.FirstName;

            if (!string.IsNullOrWhiteSpace(request.LastName))
                owner.LastName = request.LastName;

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
                owner.PhoneNumber = request.PhoneNumber;

            await _repo.UpdateAsync(owner, ct);

            return _mapper.Map<OwnerDto>(owner);
        }
    }
}