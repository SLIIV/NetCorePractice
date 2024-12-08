using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCorePractice.Models;

namespace NetCorePractice.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            var users = GetUsers();
            return View(users);
        }

        private static List<UserModel> GetUsers()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Users.ToList();
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserModel user)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                if (ModelState.IsValid)
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                if (id != null)
                {
                    UserModel user = new UserModel() { Id = id.Value};
                    context.Entry(user).State = EntityState.Deleted;
                    await context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                if (id != null)
                {
                    UserModel? user = await context.Users.FirstOrDefaultAsync(p => p.Id == id);
                    if (user != null) return View(user);
                }
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserModel user)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Users.Update(user);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }
    }
}
