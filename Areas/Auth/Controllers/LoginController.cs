using Microsoft.AspNetCore.Mvc;
using NetCorePractice.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace NetCorePractice.Areas.Authorization.Controllers
{
    [Area("Auth")]
    public class LoginController : Controller
    {
        [Route("{area}/{controller}")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Route("{area}/{controllers}/{action}")]
        [HttpPost]
        public IResult Login([FromBody]UserModel loginData)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                Encryptor encryptor = new Encryptor();
                UserModel? user = context.Users.FirstOrDefault(p => p.Email == loginData.Email && p.Password == loginData.Password);
                if (user == null) return Results.BadRequest();
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email) };
                var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                var response = new
                {
                    access_token = encodedJwt,
                    userName = user.Email
                };
                Response.Cookies.Append("accessToken", encodedJwt);
                return Results.Json(response);
            }
        }
    }
}
