using Membership.Shared.ValueObjects;

namespace Membership.UserManagerService.AspNetIdentity.Services;
internal partial class UserManagerService
{
    public async Task<IEnumerable<MembershipError>> RegisterUserAsync(UserDTO userDto)
    {
        IEnumerable<MembershipError> _errors = null;

        var user = new User
        {
            UserName = userDto.Email,
            Email = userDto.Email,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
        };

        var result = await _userManager.CreateAsync(user, userDto.Password);
        if (!result.Succeeded)
        {
            _errors = _errorsHandler.Handle(result.Errors);
        }

        return _errors;
    }

    public async Task<UserEntity> GetUserByCredentialsAsync(UserCredentialsDTO userCredentials)
    {
        UserEntity foundUser = default;
        var user = await _userManager.FindByNameAsync(userCredentials.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, userCredentials.Password))
        {
            foundUser = new UserEntity(user.FirstName, user.LastName, user.Email);

        };

        return foundUser;
    }
}
