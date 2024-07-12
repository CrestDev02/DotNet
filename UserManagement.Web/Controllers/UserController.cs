using Microsoft.AspNetCore.Mvc;
using UserManagement.Database.Entity.DataAccess.Models;
using UserManagement.Web.BusinessLogic.Interfaces;
using UserManagement.Web.Filters;

namespace UserManagement.Web.Controllers
{
    [AuthenticateFilter]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View(_userService.GetUsers().Result.ToList());
        }

        public IActionResult Details(int id)
        {
            return View(id == 0 ? new UserModel() : _userService.GetUser(id).Result);
        }

        public IActionResult Save(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _userService.Save(model);
            }
            
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var resutl = _userService.Delete(id);
            return Json(new { error = resutl.Result.Error, message = resutl.Result.Message });
        }
    }
}
