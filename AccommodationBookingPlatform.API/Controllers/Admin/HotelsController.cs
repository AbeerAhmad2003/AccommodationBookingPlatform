using AccommodationBookingPlatform.Application.Features.Hotels.Commands.CreateHotel;
using AccommodationBookingPlatform.Application.Features.Hotels.Commands.DeleteHotel;
using AccommodationBookingPlatform.Application.Features.Hotels.Commands.UpdateHotel;
using AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetHotelById;
using AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetHotels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationBookingPlatform.API.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class HotelsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HotelsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ============================
        // GET /api/admin/hotels
        // ============================
        [HttpGet]
        public async Task<IActionResult> GetHotels(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null)
        {
            var result = await _mediator.Send(
                new GetHotelsQuery(pageNumber, pageSize, search));

            return Ok(result);
        }

        // ============================
        // GET /api/admin/hotels/{id}
        // ============================
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetHotelById(Guid id)
        {
            var result = await _mediator.Send(new GetHotelByIdQuery(id));
            return Ok(result);
        }

        // ============================
        // POST /api/admin/hotels
        // ============================
        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetHotelById),
                new { id = result.Id },
                result);
        }

        // ============================
        // PUT /api/admin/hotels/{id}
        // ============================
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateHotel(Guid id, [FromBody] UpdateHotelCommand command)
        {
            if (id != command.Id)
                return BadRequest("Route id does not match request body id.");

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        // ============================
        // DELETE /api/admin/hotels/{id}
        // ============================
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteHotel(Guid id)
        {
            await _mediator.Send(new DeleteHotelCommand(id));
            return NoContent();
        }
    }
}
