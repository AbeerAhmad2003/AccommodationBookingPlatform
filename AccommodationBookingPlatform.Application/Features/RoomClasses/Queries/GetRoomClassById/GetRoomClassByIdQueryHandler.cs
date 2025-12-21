using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Application.Features.RoomClasses.Common;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Queries.GetRoomClassById
{
    public class GetRoomClassByIdQueryHandler
          : IRequestHandler<GetRoomClassByIdQuery, RoomClassDto>
    {
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IMapper _mapper;

        public GetRoomClassByIdQueryHandler(
            IRoomClassRepository roomClassRepository,
            IMapper mapper)
        {
            _roomClassRepository = roomClassRepository;
            _mapper = mapper;
        }

        public async Task<RoomClassDto> Handle(
            GetRoomClassByIdQuery request,
            CancellationToken cancellationToken)
        {
            var roomClass = await _roomClassRepository
                .GetByIdWithDetailsAsync(request.Id, cancellationToken);

            if (roomClass is null)
                throw new NotFoundException(nameof(RoomClass), request.Id);

            var dto = _mapper.Map<RoomClassDto>(roomClass);
            return dto;
        }
    }
}
