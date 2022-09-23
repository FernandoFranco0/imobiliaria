using RealState.Helper;
using RealState.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealState.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            var property = new RealStateService.Property();
            var propertyList = property.MostRecent(3);

            return View(propertyList);
        }

        public PartialViewResult GetFooterPartialView()
        {
            return PartialView("_Footer");
        }
    }
}