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
            var PropertyList = Property.List();


            if (!String.IsNullOrEmpty(parameters.State)) PropertyList = PropertyList.Where(s => s.State == parameters.State).ToList();

            if (!String.IsNullOrEmpty(parameters.City)) PropertyList = PropertyList.Where(s => s.City == parameters.City).ToList();

            if (!String.IsNullOrEmpty(parameters.NeighboorHood)) PropertyList = PropertyList.Where(s => s.NeighboorHood == parameters.NeighboorHood).ToList();

            if (!String.IsNullOrEmpty(parameters.StreetName)) PropertyList = PropertyList.Where(s => s.StreetName == parameters.StreetName).ToList();

            if (parameters.Area.HasValue) PropertyList = PropertyList.Where(s => s.Area == parameters.Area).ToList();

            if (parameters.Price.HasValue) PropertyList = PropertyList.Where(s => s.Price <= parameters.Price).ToList();

            if (parameters.BedroomNumber.HasValue) PropertyList = PropertyList.Where(s => s.BedroomNumber >= parameters.BedroomNumber).ToList();

            if (parameters.GarageSpace.HasValue) PropertyList = PropertyList.Where(s => s.GarageSpace >= parameters.GarageSpace).ToList();

            return View(PropertyList);
        }

        public PartialViewResult GetSearchBarPartialView()
        {
            var Search = new SearchModel();
            return PartialView("_SearchBar", Search);
        }

        
    }
}