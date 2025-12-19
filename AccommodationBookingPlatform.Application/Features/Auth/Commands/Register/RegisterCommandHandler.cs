using AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services;
using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Domain.Common.Enums;
using AccommodationBookingPlatform.Domain.Entities;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler
    : IRequestHandler<RegisterCommand, RegisterResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwt;

        public RegisterCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwt)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwt = jwt;
        }

        public async Task<RegisterResponse> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken)
        {
            // 1️⃣ Check email exists
            var existingUser = await _userRepository
                .GetByEmailAsync(request.Email, cancellationToken);

            if (existingUser is not null)
                throw new ValidationException("Email already exists");

            // 2️⃣ Hash password
            var passwordHash = _passwordHasher.Hash(request.Password);

            // 3️⃣ Create user
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = passwordHash,
                Role = UserRole.User,
                CreatedAtUtc = DateTime.UtcNow
            };

            // 4️⃣ Save
            await _userRepository.AddAsync(user, cancellationToken);

            // 5️⃣ Generate Token
            var token = _jwt.GenerateToken(user);

            // 6️⃣ Return
            return new RegisterResponse
            {
                UserId = user.Id,
                Email = user.Email,
                Token = token
            };
        }
    }
}
