using AccommodationBookingPlatform.API.Contracts.Auth;
using AccommodationBookingPlatform.Application.Features.Auth.Commands.Login;
using AccommodationBookingPlatform.Application.Features.Auth.Commands.Register;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationBookingPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Registers a new user and returns a JWT token.
        /// </summary>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<RegisterResponse>> Register(
            [FromBody] RegisterRequest request,
            CancellationToken cancellationToken)
        {
            var command = _mapper.Map<RegisterCommand>(request);

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);   // RegisterResponse from Application
        }

        /// <summary>
        /// Logs in a user and returns a JWT token.
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Login(
            [FromBody] LoginRequest request,
            CancellationToken cancellationToken)
        {
            var command = _mapper.Map<LoginCommand>(request);

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);   // LoginResponse from Application
        }

    }
}

