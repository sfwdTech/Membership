namespace Membership.Core.Services;
internal class AccessTokenService : IAccessTokenService
{
    readonly JwtOptions _jwtOptions;

    public AccessTokenService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public Task<string> GetNewUserAccessTokenAsync(UserEntity userEntity)
    {
        var claims = GetUserClaims(userEntity);
        return Task.FromResult(GetAccessToken(claims));
    }

    public Task<string> RotateAccessTokenAsync(string accessTokenToRotate)
    {
        var claims = GetUserClaimsFromAccessToken(accessTokenToRotate);
        return Task.FromResult(GetAccessToken(claims));
    }

    string GetAccessToken(List<Claim> userClaims)
    {
        var key = Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey);
        var secret = new SymmetricSecurityKey(key);
        var signingCredentials = new SigningCredentials(secret,
            SecurityAlgorithms.HmacSha256);
        var tokenOptions = new JwtSecurityToken(
            issuer: _jwtOptions.ValidIssuer,
            audience: _jwtOptions.ValidAudience,
            claims: userClaims,
            expires: DateTime.Now.AddMinutes(_jwtOptions.ExpireInMinutes),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);

    }

    static List<Claim> GetUserClaims(UserEntity userEntity)
    {
        return new()
        {
            new Claim(ClaimTypes.Name, userEntity.Email),
            new Claim("FullName", $"{userEntity.FullName}")
        };
    }

    static List<Claim> GetUserClaimsFromAccessToken(string accessToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.ReadJwtToken(accessToken);
        return token.Claims.ToList();
    }
}
