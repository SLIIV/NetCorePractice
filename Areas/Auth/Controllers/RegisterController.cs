using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NetCorePractice.Models;
using Microsoft.AspNetCore.Identity;

namespace NetCorePractice.Areas.Auth.Controllers
{
    [AllowAnonymous]
    [Area("Auth")]
    public class RegisterController : Controller
    {
        [Route("{area}/{controller}")]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/");
            return View();
        }


        [Route("{area}/{controller}/{action}")]
        [HttpPost]
        public IResult Create([FromBody]UserModel regData)
        {
            Encryptor encryptor = new Encryptor();
            using (ApplicationContext context = new ApplicationContext())
            {
                if (context.Users.FirstOrDefault(p => p.Email == regData.Email) != null)
                {
                    return Results.BadRequest();
                }
                regData.Password = encryptor.Enctypt(regData.Password);
                context.Users.Add(regData);
                context.SaveChanges();
                return Results.Redirect("/auth/login");
            }
        }
    }
}
