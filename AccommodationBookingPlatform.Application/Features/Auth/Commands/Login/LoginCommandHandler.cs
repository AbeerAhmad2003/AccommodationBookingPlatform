using AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services;
using AccommodationBookingPlatform.Application.Contracts.Persistence;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler
     : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwt;

        public LoginCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwt)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwt = jwt;
        }

        public async Task<LoginResponse> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository
                .GetByEmailAsync(request.Email, cancellationToken);

            if (user is null)
                throw new UnauthorizedException("Invalid email or password");

            var valid = _passwordHasher.Verify(request.Password, user.PasswordHash);

            if (!valid)
                throw new UnauthorizedException("Invalid email or password");

            var token = _jwt.GenerateToken(user);

            return new LoginResponse
            {
                UserId = user.Id,
                Email = user.Email,
                Token = token
            };
        }
    }

}
