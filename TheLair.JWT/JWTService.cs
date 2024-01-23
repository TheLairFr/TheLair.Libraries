using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TheLair.Extensions.Object;

namespace TheLair.JWT;

public class JWTService : BaseJWTService
{
    public JWTService(JWTConfig config) : base(config) { }
}