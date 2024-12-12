using Microsoft.AspNetCore.Mvc;

namespace NetCorePractice.Areas.Authorization.Controllers
{
    [Area("Authorization")]
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
