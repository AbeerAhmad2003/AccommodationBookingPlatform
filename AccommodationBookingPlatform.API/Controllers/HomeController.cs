using AccommodationBookingPlatform.Application.Features.Cities.Queries.GetTrendingCities;
using AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetFeaturedDeals;
using AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetRecentlyVisitedHotel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationBookingPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Featured Deals Section
        [HttpGet("featured-deals")]
        public async Task<IActionResult> GetFeaturedDeals([FromQuery] int count = 5)
        {
            var result = await _mediator.Send(new GetFeaturedDealsQuery(count));
            return Ok(result);
        }

        // Trending Destination
        [HttpGet("trending-cities")]
        public async Task<IActionResult> GetTrendingCities([FromQuery] int top = 5)
        {
            var result = await _mediator.Send(new GetTrendingCitiesQuery(top));
            return Ok(result);
        }

        //User's Recently Visited Hotels
        [Authorize]
        [HttpGet("recently-visited")]
        public async Task<IActionResult> GetRecentlyVisited([FromQuery] int count = 5)
        {
            var query = new GetRecentlyVisitedHotelsQuery { Count = count };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}

