using AccommodationBookingPlatform.Application.Features.Owners.Commands.CreateOwner;
using AccommodationBookingPlatform.Application.Features.Owners.Commands.DeleteOwner;
using AccommodationBookingPlatform.Application.Features.Owners.Commands.UpdateOwner;
using AccommodationBookingPlatform.Application.Features.Owners.DTOs;
using AccommodationBookingPlatform.Application.Features.Owners.Queries.GetOwnerById;
using AccommodationBookingPlatform.Application.Features.Owners.Queries.GetOwners;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationBookingPlatform.API.Controllers.Admin
{
    [Route("api/admin/owners")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class OwnersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OwnersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ✅ Get Owners (with pagination + search optional later)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OwnerDto>>> GetOwners(
            CancellationToken ct)
        {
            var result = await _mediator.Send(new GetOwnersQuery(), ct);
            return Ok(result);
        }

        // ✅ Get Owner By Id
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OwnerDto>> GetById(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetOwnerByIdQuery(id), ct);
            return Ok(result);
        }

        // ✅ Create Owner
        [HttpPost]
        public async Task<ActionResult<OwnerDto>> Create(
            [FromBody] CreateOwnerCommand command,
            CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // ✅ Update Owner
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<OwnerDto>> Update(
            Guid id,
            [FromBody] UpdateOwnerCommand command,
            CancellationToken ct)
        {
            if (id != command.Id)
                return BadRequest("Id in route must match request Id");

            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        // ✅ Delete Owner
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _mediator.Send(new DeleteOwnerCommand(id), ct);
            return NoContent();
        }
    }
}
