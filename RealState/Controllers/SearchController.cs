using RealState.Helper;
using RealState.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealState.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index(SearchModel parameters)
        {
            var Property = new RealStateService.Property();
            var PropertyList = Property.List(parameters);

            return View(PropertyList);
        }

        public PartialViewResult GetSearchBarPartialView()
        {
            var Search = new SearchModel();
            return PartialView("_SearchBar", Search);
        }

        
    }
}