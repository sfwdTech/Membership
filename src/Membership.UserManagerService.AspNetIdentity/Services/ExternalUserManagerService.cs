namespace Membership.UserManagerService.AspNetIdentity.Services;
internal partial class UserManagerService
{
    public async Task<IEnumerable<MembershipError>> RegisterExternalUserAsync(ExternalUserEntity externalUser)
    {
        IEnumerable<MembershipError> _errors = null;
        IdentityResult _result = null;
        bool userExists = false;

        var existingUser = await _userManager.FindByEmailAsync(externalUser.Email);

        if (existingUser == null)
        {
            existingUser = new User
            {
                UserName = externalUser.Email,
                Email = externalUser.Email,
                FirstName = externalUser.FirstName,
                LastName = externalUser.LastName,
            };

            _result = await _userManager.CreateAsync(existingUser);
            userExists = _result.Succeeded;
        }
        else
        {
            userExists = true;
        }

        if (userExists)
        {
            _result = await _userManager.AddLoginAsync(existingUser,
                new UserLoginInfo(externalUser.LogingProvider,
                externalUser.ProviderUserId, externalUser.LogingProvider));
        }

        if (_result != null && !_result.Succeeded)
            _errors = _errorsHandler.Handle(_result.Errors);

        return _errors;
    }

    public async Task<UserEntity> GetUserByExternalCredentialsAsync(ExternalUserCredentials externalUserCredentials)
    {
        UserEntity foundUser = null;

        var user = await _userManager.FindByLoginAsync(externalUserCredentials.LoginProvider,
            externalUserCredentials.ProviderUserId);

        if (user != null)
            foundUser = new UserEntity(user.FirstName, user.LastName, user.Email);

        return foundUser;
    }
}
