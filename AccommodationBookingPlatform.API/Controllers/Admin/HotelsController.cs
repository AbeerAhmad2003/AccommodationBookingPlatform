using AccommodationBookingPlatform.API.Contracts.Hotels;
using AccommodationBookingPlatform.Application.Features.Hotels.Commands.CreateHotel;
using AccommodationBookingPlatform.Application.Features.Hotels.Commands.DeleteHotel;
using AccommodationBookingPlatform.Application.Features.Hotels.Commands.UpdateHotel;
using AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetHotelById;
using AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetHotels;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public HotelsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET
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

        // GET BY ID
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetHotelById(Guid id)
        {
            var result = await _mediator.Send(new GetHotelByIdQuery(id));
            return Ok(result);
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> CreateHotel(CreateHotelRequest request)
        {
            var command = _mapper.Map<CreateHotelCommand>(request);

            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetHotelById),
                new { id = result.Id },
                result);
        }

        // UPDATE
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateHotel(Guid id, UpdateHotelRequest request)
        {
            var command = _mapper.Map<UpdateHotelCommand>(request);

            command = command with { Id = id };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        // DELETE
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteHotel(Guid id)
        {
            await _mediator.Send(new DeleteHotelCommand(id));
            return NoContent();
        }
    }
}
