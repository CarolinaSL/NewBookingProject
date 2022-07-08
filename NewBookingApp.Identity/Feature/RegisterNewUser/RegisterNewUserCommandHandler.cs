using MassTransit;
using Microsoft.AspNetCore.Identity;
using NewBookingApp.Core.Contracts;
using NewBookingApp.Core.CQRS;
using NewBookingApp.Identity.Dtos;
using NewBookingApp.Identity.Models;
using NewBookingApp.Identity.Models.Constants;

namespace NewBookingApp.Identity.Feature.RegisterNewUser
{
    public class RegisterNewUserCommandHandler : ICommandHandler<RegisterNewUserCommand, RegisterNewUserResponseDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPublishEndpoint _publishEndpoint;

        public RegisterNewUserCommandHandler(UserManager<ApplicationUser> userManager, IPublishEndpoint publishEndpoint)
        {
            _userManager = userManager;
            _publishEndpoint = publishEndpoint;
           
        }

        public async Task<RegisterNewUserResponseDto> Handle(RegisterNewUserCommand command, CancellationToken cancellationToken)
        {
            var applicationUser = new ApplicationUser
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                UserName = command.Username,
                Email = command.Email,
                PasswordHash = command.Password,
                PassPortNumber = command.PassportNumber
            };

            var identityResult = await _userManager.CreateAsync(applicationUser, command.Password);
            var roleResult = await _userManager.AddToRoleAsync(applicationUser, Constants.Role.User);

            if (identityResult.Succeeded == false)
                throw new NotImplementedException(string.Join(',', identityResult.Errors.Select(e => e.Description)));

            if (roleResult.Succeeded == false)
                throw new NotImplementedException(string.Join(',', roleResult.Errors.Select(e => e.Description)));

            await _publishEndpoint.Publish<UserCreated>(new UserCreated(applicationUser.Id, applicationUser.FirstName + " " + applicationUser.LastName,
                 applicationUser.PassPortNumber));


            return new RegisterNewUserResponseDto
            {
                Id = applicationUser.Id,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                Username = applicationUser.UserName,
                PassportNumber = applicationUser.PassPortNumber
            };

        }
    }
}
