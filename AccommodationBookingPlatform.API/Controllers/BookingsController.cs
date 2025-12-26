using AccommodationBookingPlatform.API.Contracts.Bookings;
using AccommodationBookingPlatform.Application.Features.Bookings.Commands.CreateBooking;
using AccommodationBookingPlatform.Application.Features.Bookings.Commands.DeleteBooking;
using AccommodationBookingPlatform.Application.Features.Bookings.Commands.UpdateBooking;
using AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetAllBookings;
using AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetBookingDetails;
using AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetPastBookings;
using AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetUpcomingBookings;
using AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetUserBookings;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationBookingPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BookingsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // ===========================
        // GET Booking By Id
        // ===========================
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BookingDetailsDto>> GetById(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetBookingDetailsQuery(id), ct);
            return Ok(result);
        }

        // ===========================
        // GET User Bookings
        // ===========================
        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<UserBookingListItemDto>>> GetUserBookings(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetUserBookingsQuery(), ct);
            return Ok(result);
        }

        [HttpGet("user/upcoming")]
        public async Task<ActionResult<IEnumerable<UserBookingListItemDto>>> GetUpcoming(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetUpcomingBookingsQuery(), ct);
            return Ok(result);
        }

        [HttpGet("user/past")]
        public async Task<ActionResult<IEnumerable<UserBookingListItemDto>>> GetPast(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetPastBookingsQuery(), ct);
            return Ok(result);
        }

        // ===========================
        // Admin Paginated
        // ===========================
        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAdminBookings(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await _mediator.Send(
                new GetAdminBookingsQuery(pageNumber, pageSize),
                ct
            );

            return Ok(result);
        }

        // ===========================
        // CREATE Booking
        // ===========================
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateBookingRequest request, CancellationToken ct)
        {
            var command = _mapper.Map<CreateBookingCommand>(request);

            var bookingId = await _mediator.Send(command, ct);

            return CreatedAtAction(
                nameof(GetById),
                new { id = bookingId },
                new { Id = bookingId }
            );
        }

        // ===========================
        // UPDATE Booking
        // ===========================
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<BookingDetailsDto>> Update(Guid id, UpdateBookingRequest request, CancellationToken ct)
        {
            var command = _mapper.Map<UpdateBookingCommand>(request);
            command = command with { BookingId = id };

            await _mediator.Send(command, ct);

            return Ok(new { Id = id });
        }

        // ===========================
        // DELETE Booking
        // ===========================
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _mediator.Send(new DeleteBookingCommand(id), ct);

            return NoContent();
        }


    }
}