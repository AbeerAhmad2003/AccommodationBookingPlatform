using AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetHotelDetails;
using AccommodationBookingPlatform.Application.Features.Hotels.Queries.SearchHotels;
using AccommodationBookingPlatform.Domain.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationBookingPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HotelsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //  Search Results Page
        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] Guid? cityId,
            [FromQuery] string? cityName,
            [FromQuery] double? minStars,
            [FromQuery] double? maxStars,
            [FromQuery] DateTime? checkIn,
            [FromQuery] DateTime? checkOut,
            [FromQuery] int rooms = 1,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? sortColumn = "Rating",
            [FromQuery] SortOrder sortOrder = SortOrder.Desc)
        {
            var query = new SearchHotelsQuery(
                cityId,
                cityName,
                minStars,
                maxStars,
                checkIn,
                checkOut,
                rooms,
                pageNumber,
                pageSize,
                sortColumn,
                sortOrder
            );

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // Hotel Page (Details + Gallery + Availability + Reviews)
        [HttpGet("{hotelId:guid}")]
        public async Task<IActionResult> GetDetails(
            Guid hotelId,
            [FromQuery] DateTime? checkIn,
            [FromQuery] DateTime? checkOut,
            [FromQuery] int rooms = 1)
        {
            var query = new GetHotelDetailsQuery(hotelId, checkIn, checkOut, rooms);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }

}
