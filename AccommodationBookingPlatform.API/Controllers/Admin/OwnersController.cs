using AccommodationBookingPlatform.API.Contracts.Owners;
using AccommodationBookingPlatform.Application.Features.Owners.Commands.CreateOwner;
using AccommodationBookingPlatform.Application.Features.Owners.Commands.DeleteOwner;
using AccommodationBookingPlatform.Application.Features.Owners.Commands.UpdateOwner;
using AccommodationBookingPlatform.Application.Features.Owners.DTOs;
using AccommodationBookingPlatform.Application.Features.Owners.Queries.GetOwnerById;
using AccommodationBookingPlatform.Application.Features.Owners.Queries.GetOwners;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public OwnersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET Owners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OwnerDto>>> GetOwners(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetOwnersQuery(), ct);
            return Ok(result);
        }

        // GET By Id
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OwnerDto>> GetById(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetOwnerByIdQuery(id), ct);
            return Ok(result);
        }

        // CREATE
        [HttpPost]
        public async Task<ActionResult<OwnerDto>> Create(CreateOwnerRequest request, CancellationToken ct)
        {
            var command = _mapper.Map<CreateOwnerCommand>(request);

            var result = await _mediator.Send(command, ct);

            return CreatedAtAction(nameof(GetById),
                new { id = result.Id },
                result);
        }

        // UPDATE (Partial Supported)
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<OwnerDto>> Update(Guid id, UpdateOwnerRequest request, CancellationToken ct)
        {
            if (id != request.Id)
                return BadRequest("Id in route must match request Id");

            var command = _mapper.Map<UpdateOwnerCommand>(request);

            var result = await _mediator.Send(command, ct);

            return Ok(result);
        }

        // DELETE
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _mediator.Send(new DeleteOwnerCommand(id), ct);
            return NoContent();
        }
    }
}