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
    [Route("api/bookings")]
    [Authorize] // لازم يكون User أو Admin مسجّل دخول
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
        // 1️⃣ Create Booking
        // ===========================
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateBookingRequest request,
            CancellationToken ct)
        {
            var command = _mapper.Map<CreateBookingCommand>(request);

            var bookingId = await _mediator.Send(command, ct);

            return Ok(new
            {
                BookingId = bookingId,
                Message = "Booking created successfully"
            });
        }

        // ===========================
        // 2️⃣ Update Booking
        // ===========================
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            UpdateBookingRequest request,
            CancellationToken ct)
        {
            var command = _mapper.Map<UpdateBookingCommand>(request);
            command = command with { BookingId = id };

            await _mediator.Send(command, ct);

            return Ok(new
            {
                BookingId = id,
                Message = "Booking updated successfully"
            });
        }

        // ===========================
        // 3️⃣ Delete Booking
        // User + Admin
        // ===========================
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _mediator.Send(new DeleteBookingCommand(id), ct);

            return Ok(new
            {
                BookingId = id,
                Message = "Booking deleted successfully"
            });
        }

        // ===========================
        // Booking Details
        // ===========================
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetDetails(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetBookingDetailsQuery(id), ct);
            return Ok(result);
        }

        // ===========================
        // 5Get User Bookings
        // ===========================
        [HttpGet("user")]
        public async Task<IActionResult> GetUserBookings(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetUserBookingsQuery(), ct);
            return Ok(result);
        }

        // ===========================
        // 6Upcoming Bookings
        // ===========================
        [HttpGet("user/upcoming")]
        public async Task<IActionResult> GetUpcoming(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetUpcomingBookingsQuery(), ct);
            return Ok(result);
        }

        // ===========================
        // Past Bookings
        // ===========================
        [HttpGet("user/past")]
        public async Task<IActionResult> GetPast(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetPastBookingsQuery(), ct);
            return Ok(result);
        }

        // ===========================
        // Admin Paginated Bookings
        // Dashboard Grid
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
    }
}
