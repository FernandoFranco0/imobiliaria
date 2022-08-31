using RealState.Helper;
using RealState.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace RealState.Controllers
{
    public class UserController : Controller
    {
        [Authorize]
        // GET: User
        public ActionResult Index(int? UserId)
        {
            var UserService = new RealStateService.User();

            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            var claims = identity.Claims.ToList();

            if (String.Equals(claims[3].Value, "1"))
            {
                if (UserId.HasValue) return View("SingleUser", UserService.Get((int)UserId));

                var ViewModel = UserService.List();
                return View("User", ViewModel);
            }

            if (UserId.HasValue)
            {
                if (String.Equals(claims[0].Value, UserId.Value.ToString()))
                {
                    return View("SingleUser", UserService.Get((int)UserId));
                }
            }

            return RedirectToAction("Index", "Home");
        }


        [Authorize]
        [HttpGet]
        public ActionResult AddUpdate(int? id)
        {
            var User = new RealStateService.User();

            if (id.HasValue) return View(User.Get((int)id));

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddUpdate(UserModel User)
        {
            var OtherUser = new RealStateService.User();

            if (!OtherUser.Update(User))
            {
                OtherUser.Add(User);
            }

            return RedirectToAction("Index", new { UserId = User.Id });
        }

        [Authorize]
        public ActionResult Remove(int id)
        {
            var UserRemove = new RealStateService.User();
            UserRemove.Remove(id);
            return RedirectToAction("Index");
        }
    }
}