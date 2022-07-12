using NewBookingApp.Core.CQRS;
using NewBookingApp.Identity.Dtos;

namespace NewBookingApp.Identity.Feature.RegisterNewUser
{
    public record RegisterNewUserCommand(string FirstName, string LastName, string Username, string Email,
     string Password, string ConfirmPassword, string PassportNumber) : ICommand<RegisterNewUserResponseDto>;

}
