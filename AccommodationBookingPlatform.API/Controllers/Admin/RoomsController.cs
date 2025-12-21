using AccommodationBookingPlatform.Application.Features.Rooms.Commands.CreateRoom;
using AccommodationBookingPlatform.Application.Features.Rooms.Commands.DeleteRoom;
using AccommodationBookingPlatform.Application.Features.Rooms.Common;
using AccommodationBookingPlatform.Application.Features.Rooms.Queries.GetRoomsByRoomClass;
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

        public RoomsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ============================================
        // GET: /api/admin/room-classes/{roomClassId}/rooms
        // ============================================
        [HttpGet("~/api/admin/room-classes/{roomClassId:guid}/rooms")]
        public async Task<ActionResult<IReadOnlyList<RoomDto>>> GetByRoomClass(Guid roomClassId)
        {
            var result = await _mediator.Send(
                new GetRoomsByRoomClassQuery(roomClassId));

            return Ok(result);
        }

        // ============================================
        // POST: /api/admin/rooms
        // ============================================
        [HttpPost]
        public async Task<ActionResult<RoomDto>> Create([FromBody] RoomInputDto dto)
        {
            var result = await _mediator.Send(
                new CreateRoomCommand(dto));

            return CreatedAtAction(
                nameof(GetByRoomClass),
                new { roomClassId = result.RoomClassId },
                result
            );
        }

        // ============================================
        // DELETE: /api/admin/rooms/{id}
        // ============================================
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteRoomCommand(id));
            return NoContent();
        }
    }
}
