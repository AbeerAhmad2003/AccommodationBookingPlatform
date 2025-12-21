using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Domain.Entities;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Cities.Commands.DeleteCity
{
    public class DeleteCityCommandHandler
     : IRequestHandler<DeleteCityCommand, Unit>
    {
        private readonly ICityRepository _repo;

        public DeleteCityCommandHandler(ICityRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(DeleteCityCommand request, CancellationToken ct)
        {
            var city = await _repo.GetByIdAsync(request.Id, ct);

            if (city is null)
                throw new NotFoundException(nameof(City), request.Id);

            await _repo.DeleteAsync(city, ct);

            return Unit.Value;
        }
    }

}
