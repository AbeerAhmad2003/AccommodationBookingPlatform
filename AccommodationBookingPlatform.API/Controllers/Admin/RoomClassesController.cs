using AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.CreateRoomClass;
using AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.DeleteRoomClass;
using AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.UpdateRoomClass;
using AccommodationBookingPlatform.Application.Features.RoomClasses.Common;
using AccommodationBookingPlatform.Application.Features.RoomClasses.Queries.GetRoomClassById;
using AccommodationBookingPlatform.Application.Features.RoomClasses.Queries.GetRoomClassesByHotel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationBookingPlatform.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]")]
    [Authorize(Roles = "Admin")]
    public class RoomClassesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomClassesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET /api/admin/roomclasses/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<RoomClassDto>> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetRoomClassByIdQuery(id));
            return Ok(result);
        }

        // GET /api/admin/hotels/{hotelId}/room-classes
        [HttpGet("~/api/admin/hotels/{hotelId:guid}/room-classes")]
        public async Task<ActionResult<IReadOnlyList<RoomClassDto>>> GetByHotel(Guid hotelId)
        {
            var result = await _mediator.Send(new GetRoomClassesByHotelQuery(hotelId));
            return Ok(result);
        }

        // POST /api/admin/roomclasses
        [HttpPost]
        public async Task<ActionResult<RoomClassDto>> Create([FromBody] RoomClassInputDto dto)
        {
            var result = await _mediator.Send(new CreateRoomClassCommand(dto));

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }

        // PUT /api/admin/roomclasses/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<RoomClassDto>> Update(
            Guid id,
            [FromBody] UpdateRoomClassCommand command)
        {
            if (id != command.Id)
                return BadRequest("Route id does not match body id.");

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // DELETE /api/admin/roomclasses/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteRoomClassCommand(id));
            return NoContent();
        }
    }
}
