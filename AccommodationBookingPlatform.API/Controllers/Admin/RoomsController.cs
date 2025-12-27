using AccommodationBookingPlatform.API.Contracts.Rooms;
using AccommodationBookingPlatform.Application.Features.Rooms.Commands.CreateRoom;
using AccommodationBookingPlatform.Application.Features.Rooms.Commands.DeleteRoom;
using AccommodationBookingPlatform.Application.Features.Rooms.Commands.UpdateRoom;
using AccommodationBookingPlatform.Application.Features.Rooms.Common;
using AccommodationBookingPlatform.Application.Features.Rooms.Queries.GetRoomsByRoomClass;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationBookingPlatform.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]")]
    [Authorize(Roles = "Admin")]
    public class RoomsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RoomsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET /api/admin/room-classes/{roomClassId}/rooms
        [HttpGet("~/api/admin/room-classes/{roomClassId:guid}/rooms")]
        public async Task<ActionResult<IReadOnlyList<RoomDto>>> GetByRoomClass(Guid roomClassId)
        {
            var result = await _mediator.Send(
                new GetRoomsByRoomClassQuery(roomClassId));

            return Ok(result);
        }

        // POST /api/admin/rooms
        [HttpPost]
        public async Task<ActionResult<RoomDto>> Create(CreateRoomRequest request)
        {
            var command = _mapper.Map<CreateRoomCommand>(request);

            var result = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetByRoomClass),
                new { roomClassId = result.RoomClassId },
                result);
        }

        // DELETE /api/admin/rooms/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteRoomCommand(id));
            return NoContent();
        }
        // ============================================
        // PUT: /api/admin/rooms/{id}
        // ============================================
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<RoomDto>> Update(
     Guid id,
     UpdateRoomRequest request,
     CancellationToken ct)
        {
            var command = _mapper.Map<UpdateRoomCommand>(request);

            command = command with { Id = id };

            var result = await _mediator.Send(command, ct);

            return Ok(result);
        }

    }
}