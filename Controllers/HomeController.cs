using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NetCorePractice.Models;

namespace NetCorePractice.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var users = GetUsers();
        return View(users);
    }

    [HttpPost]
    public IActionResult AddUser(UserModel user)
    {
        using (ApplicationContext context = new ApplicationContext())
        {
            context.Users.Add(user);
            context.SaveChanges();
            return Redirect("/");
        }
    }

    private List<UserModel> GetUsers()
    {
        using (ApplicationContext context = new ApplicationContext())
        {
            return context.Users.ToList();
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
