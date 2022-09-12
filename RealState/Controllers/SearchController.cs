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

        public PartialViewResult GetDetailedSearchBarPartialView()
        {
            var Search = new SearchModel();
            return PartialView("_DetailedSearch", Search);
        }

        public PartialViewResult GetHorizontalImagePartialView(List<PropertyModel> Property)
        {
            return PartialView("_HorizontalImage", Property);
        }

        public PartialViewResult GetCarrouselPartialView(PropertyModel Property)
        {
            return PartialView("_Carrousel", Property);
        }

    }
}