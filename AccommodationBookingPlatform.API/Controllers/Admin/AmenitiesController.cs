using AccommodationBookingPlatform.API.Contracts.Amenities;
using AccommodationBookingPlatform.Application.Features.Amenities.Commands.CreateAmenity;
using AccommodationBookingPlatform.Application.Features.Amenities.Commands.DeleteAmenity;
using AccommodationBookingPlatform.Application.Features.Amenities.Commands.UpdateAmenity;
using AccommodationBookingPlatform.Application.Features.Amenities.Common;
using AccommodationBookingPlatform.Application.Features.Amenities.Queries.GetAllAmenities;
using AccommodationBookingPlatform.Application.Features.Amenities.Queries.GetAmenityById;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationBookingPlatform.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AmenitiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AmenitiesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // ===========================
        // GET: /api/admin/amenities
        // ===========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmenityDto>>> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllAmenitiesQuery(), ct);
            return Ok(result);
        }

        // ===========================
        // GET: /api/admin/amenities/{id}
        // ===========================
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<AmenityDto>> GetById(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAmenityByIdQuery(id), ct);
            return Ok(result);
        }

        // ===========================
        // POST: /api/admin/amenities
        // ===========================
        [HttpPost]
        public async Task<ActionResult<AmenityDto>> Create(
            CreateAmenityRequest request,
            CancellationToken ct)
        {
            var command = _mapper.Map<CreateAmenityCommand>(request);

            var result = await _mediator.Send(command, ct);

            return CreatedAtAction(nameof(GetById),
                new { id = result.Id },
                result);
        }

        // ===========================
        // PUT: /api/admin/amenities/{id}
        // ===========================
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<AmenityDto>> Update(
            Guid id,
            UpdateAmenityRequest request,
            CancellationToken ct)
        {
            var command = _mapper.Map<UpdateAmenityCommand>(request);
            command = command with { Id = id };

            var result = await _mediator.Send(command, ct);

            return Ok(result);
        }

        // ===========================
        // DELETE: /api/admin/amenities/{id}
        // ===========================
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _mediator.Send(new DeleteAmenityCommand(id), ct);
            return NoContent();
        }
    }
}
