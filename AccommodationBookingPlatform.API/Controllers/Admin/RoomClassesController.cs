using AccommodationBookingPlatform.API.Contracts.RoomClasses;
using AccommodationBookingPlatform.Application.Features.Amenities.Common;
using AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetHotelDetails;
using AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.AssignAmenityToRoomClass;
using AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.CreateRoomClass;
using AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.DeleteRoomClass;
using AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.RemoveAmenityFromRoomClass;
using AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.UpdateRoomClass;
using AccommodationBookingPlatform.Application.Features.RoomClasses.Queries.GetRoomClassAmenities;
using AccommodationBookingPlatform.Application.Features.RoomClasses.Queries.GetRoomClassById;
using AccommodationBookingPlatform.Application.Features.RoomClasses.Queries.GetRoomClassesByHotel;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/admin/[controller]")]
[Authorize(Roles = "Admin")]
public class RoomClassesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public RoomClassesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    // ===========================
    // GET RoomClass By Id
    // ===========================
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RoomClassDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetRoomClassByIdQuery(id), ct);
        return Ok(result);
    }

    // ===========================
    // GET RoomClasses by Hotel
    // ===========================
    [HttpGet("~/api/admin/hotels/{hotelId:guid}/room-classes")]
    public async Task<ActionResult<IReadOnlyList<RoomClassDto>>> GetByHotel(Guid hotelId, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetRoomClassesByHotelQuery(hotelId), ct);
        return Ok(result);
    }

    // ===========================
    // CREATE
    // ===========================
    [HttpPost]
    public async Task<ActionResult<RoomClassDto>> Create(CreateRoomClassRequest request, CancellationToken ct)
    {
        var command = _mapper.Map<CreateRoomClassCommand>(request);

        var result = await _mediator.Send(command, ct);

        return CreatedAtAction(nameof(GetById),
            new { id = result.Id },
            result);
    }

    // ===========================
    // UPDATE
    // ===========================
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<RoomClassDto>> Update(Guid id, UpdateRoomClassRequest request, CancellationToken ct)
    {
        var command = _mapper.Map<UpdateRoomClassCommand>(request);
        command = command with { Id = id };

        var result = await _mediator.Send(command, ct);

        return Ok(result);
    }

    // ===========================
    // DELETE
    // ===========================
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _mediator.Send(new DeleteRoomClassCommand(id), ct);
        return NoContent();
    }

    // ===========================
    // Get Amenities of RoomClass
    // ===========================
    [HttpGet("{roomClassId:guid}/amenities")]
    public async Task<ActionResult<IEnumerable<AmenityDto>>> GetAmenities(Guid roomClassId, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetRoomClassAmenitiesQuery(roomClassId), ct);
        return Ok(result);
    }

    // ===========================
    // Assign Amenity
    // ===========================
    [HttpPost("{roomClassId:guid}/amenities/{amenityId:guid}")]
    public async Task<IActionResult> AssignAmenity(Guid roomClassId, Guid amenityId, CancellationToken ct)
    {
        await _mediator.Send(
            new AssignAmenityToRoomClassCommand(roomClassId, amenityId),
            ct);

        return NoContent();
    }

    // ===========================
    // Remove Amenity
    // ===========================
    [HttpDelete("{roomClassId:guid}/amenities/{amenityId:guid}")]
    public async Task<IActionResult> RemoveAmenity(Guid roomClassId, Guid amenityId, CancellationToken ct)
    {
        await _mediator.Send(
            new RemoveAmenityFromRoomClassCommand(roomClassId, amenityId),
            ct);

        return NoContent();
    }
}

