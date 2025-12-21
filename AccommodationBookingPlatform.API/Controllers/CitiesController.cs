using AccommodationBookingPlatform.Application.Features.Cities.Commands.CreateCity;
using AccommodationBookingPlatform.Application.Features.Cities.Commands.DeleteCity;
using AccommodationBookingPlatform.Application.Features.Cities.Commands.UpdateCity;
using AccommodationBookingPlatform.Application.Features.Cities.Queries.GetCities;
using AccommodationBookingPlatform.Application.Features.Cities.Queries.GetCityById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationBookingPlatform.API.Controllers
{
    [ApiController]
    [Route("api/admin/cities")]
    [Authorize(Roles = "Admin")]
    public class CitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ================================
        // GET Cities (Paginated + Search)
        // ================================
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null)
        {
            var result = await _mediator.Send(
                new GetCitiesQuery(pageNumber, pageSize, search));

            return Ok(result);
        }

        // ================================
        // GET City By Id
        // ================================
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetCityByIdQuery(id));

            return Ok(result);
        }

        // ================================
        // CREATE City
        // ================================
        [HttpPost]
        public async Task<IActionResult> Create(CreateCityCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById),
                new { id = result.Id }, result);
        }

        // ================================
        // UPDATE City
        // ================================
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateCityCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id in route and body must match");

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // ================================
        // DELETE City
        // ================================
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteCityCommand(id));
            return NoContent();
        }
    }
}
