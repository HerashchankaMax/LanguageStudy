using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LangCardsDomain.Models;
using Microsoft.IdentityModel.Tokens;

namespace LangCardsAPI.Services;

public class TokenService
{
   private readonly IConfiguration _config;

   public TokenService(IConfiguration config)
   {
      _config = config;
   }
   public string CreateToken(ApplicationUser? applicationUser)
   {
      JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
      IEnumerable<Claim>? claims = new []
      {
         new Claim(ClaimTypes.Name, applicationUser.UserName),
         new Claim(ClaimTypes.Email, applicationUser.Email),
         new Claim(ClaimTypes.NameIdentifier, applicationUser.Id),
      };
      string? securityKeyValue = _config.GetSecurityKey();
      SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKeyValue));
      SigningCredentials creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256 );
      SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
      {
         Expires = DateTime.Now.AddDays(1),
         SigningCredentials = creds,
         Subject = new ClaimsIdentity(claims)
      };
      var token =  tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
   }
}