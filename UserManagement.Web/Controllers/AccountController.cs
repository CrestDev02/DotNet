using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserManagement.Web.BusinessLogic.Interfaces;
using UserManagement.Web.Models;

namespace UserManagement.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = (await _userService.GetUsers()).Where(x => x.Email == model.EmailAddress && x.Password == model.Password).FirstOrDefault();
                if (user == null)
                {
                    ViewBag.Message = "Invalid Username & Password";
                    return View();
                }
                else
                {
                    HttpContext.Session.SetString("UserDetails", JsonConvert.SerializeObject(user));
                    return RedirectToAction("Index", "User");
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
