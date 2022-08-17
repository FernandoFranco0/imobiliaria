using RealState.Helper;
using RealState.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return View(UserId);
        }

        [Authorize]
        [HttpGet]
        public PartialViewResult GetUserPartialView(int? Userid)
        {
            var User = new RealStateService.User();

            if (Userid.HasValue) return PartialView("_SingleUser", User.Get((int)Userid));

            var ViewModel = User.List();
            return PartialView("_User", ViewModel);
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

            return RedirectToAction("Index");
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