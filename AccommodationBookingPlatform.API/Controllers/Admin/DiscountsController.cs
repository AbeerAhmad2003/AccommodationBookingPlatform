using AccommodationBookingPlatform.API.Contracts.Discounts;
using AccommodationBookingPlatform.Application.Features.Discounts.Commands.CreateDiscount;
using AccommodationBookingPlatform.Application.Features.Discounts.Commands.DeleteDiscount;
using AccommodationBookingPlatform.Application.Features.Discounts.Commands.UpdateDiscount;
using AccommodationBookingPlatform.Application.Features.Discounts.Common;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationBookingPlatform.API.Controllers.Admin
{
    [Route("api/admin/discounts")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DiscountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DiscountsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create(CreateDiscountRequest request)
        {
            var command = _mapper.Map<CreateDiscountCommand>(request);

            var id = await _mediator.Send(command);

            return Ok(new { Id = id });
        }

        // UPDATE
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<DiscountDto>> Update(
      Guid id,
      UpdateDiscountRequest request)
        {
            var command = _mapper.Map<UpdateDiscountCommand>(request);
            command = command with { Id = id };

            var result = await _mediator.Send(command);

            return Ok(result);
        }


        // DELETE
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteDiscountCommand(id));
            return NoContent();
        }
    }

}
