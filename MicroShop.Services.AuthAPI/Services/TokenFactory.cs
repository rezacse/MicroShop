using Azure.Core;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MircroShop.Services.AuthAPI.Models;
using MircroShop.Services.AuthAPI.Tables;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MicroShop.Services.AuthAPI.Services;

public interface ITokenFactory
{
    string GenerateRefreshToken(int userID);
    AccessToken GenerateAccessToken(User dto);
}

public class TokenFactory : ITokenFactory
{
    private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;
    private readonly IOptions<AuthConfig> authConfig;

    public TokenFactory(IOptions<AuthConfig> authConfig)
    {
        this.authConfig = authConfig;
        if (jwtSecurityTokenHandler == null) jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    }

    #region Generate Token
    public string GenerateRefreshToken(int userID)
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        var memberStr = Convert.ToBase64String(BitConverter.GetBytes(userID));
        return $"{Convert.ToBase64String(randomNumber)}{memberStr}";
    }

    public AccessToken GenerateAccessToken(User dto)
    {
        var createTime = DateTime.UtcNow;
        Claim[] claims = GetClaims(dto, createTime);
        DateTime expiredAt = GetExpiredAt(createTime);

        var issuer = authConfig.Value.ISSUER;
        var audience = authConfig.Value.AUDIENCE;

        var signInKey = GetSignInKey();

        var jwt = new JwtSecurityToken(issuer, audience, claims, createTime, expiredAt, signInKey);
        var token = jwtSecurityTokenHandler.WriteToken(jwt);
        return new AccessToken(token, expiredAt);
    }

    private DateTime GetExpiredAt(DateTime createTime)
    {
        var jwtDuration = authConfig.Value.DURATION_IN_MINS;
        //var jwtDuration = userRoleID switch
        //{
        //    //UserRoleCategory.QUICK_COACH => authConfig.Value.APP_JWT_DURATION,
        //    _ => authConfig.Value.DURATION_IN_MINS
        //};

        return createTime.AddMinutes(jwtDuration);
    }

    private static Claim[] GetClaims(User dto, DateTime createTime)
    {
        var claims = new List<Claim>
        {
             new Claim(JwtRegisteredClaimNames.GivenName, dto.Name),
             new Claim(JwtRegisteredClaimNames.Sub, dto.Id),
             new Claim(JwtRegisteredClaimNames.UniqueName, dto.UserName ?? string.Empty),
             new Claim(JwtRegisteredClaimNames.Email, dto.Email ?? string.Empty),
             new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(createTime).ToString(), ClaimValueTypes.Integer64),
             //new Claim(JWT_CLAIMS.ROLE_ID, ((int)userRoleID).ToString()),
             //new Claim(JWT_CLAIMS.LNG_ID, dto.LngType.ToString()),
        };

        return claims.ToArray();
    }

    private SigningCredentials GetSignInKey()
    {
        var ssKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authConfig.Value.JWT_KEY));
        var signInKey = new SigningCredentials(ssKey, SecurityAlgorithms.HmacSha256);
        return signInKey;
    }

    #endregion

    public ClaimsPrincipal ValidateToken(string token, TokenValidationParameters tokenValidationParameters)
    {
        try
        {
            var principal = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
        catch (Exception)
        {
            throw new SecurityTokenException("Invalid token");
        }
    }

    #region Private Methods
    private static long ToUnixEpochDate(DateTime date)
    {
        return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
            .TotalSeconds);
    }
    #endregion


}
