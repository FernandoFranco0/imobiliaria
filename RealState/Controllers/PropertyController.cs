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
    public class PropertyController : Controller
    {
        // GET: Property
        [AllowAnonymous]
        public ActionResult Index(int PropertyId)
        {
            var Property = new RealStateService.Property().Get(PropertyId);
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            var claims = identity.Claims.ToList();

            if (Request.IsAuthenticated && (String.Equals(claims[0].Value, Property.UserId.ToString()) || String.Equals(claims[3].Value, "1")))
            {
                return View("EditableProperty", Property);
            }

            return View("ReadOnlyProperty", Property);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddUpdate(int? id, int UserId)
        {
            var Property = new RealStateService.Property();
            var NewProperty = new PropertyModel();

            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            var claims = identity.Claims.ToList();

            if (String.Equals(claims[3].Value, "1") || String.Equals(claims[0].Value, UserId.ToString()) )
            {
                if (id.HasValue) return View(Property.Get((int)id));

                NewProperty.UserId = UserId;
                return View(NewProperty);
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddUpdate2()
        {
            var Property = new RealStateService.Property();
            var NewProperty = new PropertyModel();


            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            var claims = identity.Claims.ToList();

            var UserId = Int32.Parse(claims[0].Value) ;

            if (String.Equals(claims[3].Value, "1") || String.Equals(claims[0].Value, UserId.ToString()))
            {

                NewProperty.UserId = UserId;
                return View(NewProperty);
            }

            return RedirectToAction("Index", "Home");
        }


        [Authorize]
        [HttpPost]
        public ActionResult Add(PropertyModel Property)
        {
            var NewProperty = new RealStateService.Property();

            if (String.IsNullOrEmpty(Property.ImagesUrl[0]))
            {
                ModelState.AddModelError("ImagesUrl", "Envie pelo menos uma foto");
                return View("AddUpdate", Property);
            }

            int propertyId = NewProperty.Add(Property);


            var Image = new RealStateService.Image();

            var NewImage = new ImageModel();
            NewImage.PropertyId = propertyId;

            foreach (var photo in Property.ImagesUrl)
            {
                NewImage.ImageUrl = photo;
                Image.Add(NewImage);
            }


            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Update(PropertyModel Property)
        {

            var PropertyService = new RealStateService.Property();
            PropertyService.Update(Property);

            var Image = new RealStateService.Image();

            var NewImage = new ImageModel();
            NewImage.PropertyId = Property.Id;

            foreach (var photo in Property.ImagesUrl)
            {
                NewImage.ImageUrl = photo;
                Image.Add(NewImage);
            }

            return RedirectToAction("Index", "Property", new { @PropertyId = Property.Id });         

        }


        [Authorize]
        public ActionResult Remove(int Propertyid)
        {
            var PropertyRemove = new RealStateService.Property();
            PropertyRemove.Remove(Propertyid);
            return RedirectToAction("Index" , "Home");
        }

        public PartialViewResult GetImagePartialView(List<PropertyModel> Property)
        {
            return PartialView("_Image", Property);
        }

        public PartialViewResult ButtonPartialView(int PropertyId )
        {
            var Favorite = new RealStateService.Favorite();

            var FavoriteViewModel = new FavoriteModel
            {
                PropertyId = PropertyId,
            };

            if (!Request.IsAuthenticated)
                return PartialView("_NotFavoriteButton", FavoriteViewModel);


            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            var claims = identity.Claims.ToList();
            var UserId = Int32.Parse(claims[0].Value);

            

            var IsFavorite = Favorite.IsFavorite(UserId, PropertyId);

            if (IsFavorite == null)
                return PartialView("_NotFavoriteButton", FavoriteViewModel);

            return PartialView("_IsFavoriteButton", FavoriteViewModel);
            
        }

        [Authorize]
        [HttpGet]
        public PartialViewResult ChangeFavoriteButton(int PropertyId)
        {

            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            var claims = identity.Claims.ToList();
            var UserId = Int32.Parse(claims[0].Value);

            var FavoriteHelper = new RealStateService.Favorite();         
            var IsFavorite = FavoriteHelper.IsFavorite(UserId, PropertyId);          
            var Favorite = new FavoriteModel
            {
                PropertyId = PropertyId,
                UserId = UserId
            };

            if (IsFavorite == null)
            {
                FavoriteHelper.Add(Favorite);
                return PartialView("_IsFavoriteButton", Favorite);
            }

            FavoriteHelper.Remove(IsFavorite.Id);
            return PartialView("_NotFavoriteButton" , Favorite);
        }

        [Authorize]
        public ActionResult RemoveImage(int ImageId, int PropertyId)
        {
            var PropertyHelper = new RealStateService.Property();
            var Property = PropertyHelper.Get(PropertyId);

            if (Property.ImagesUrl.Count > 1)
            {
                var ImagageRemove = new RealStateService.Image();
                ImagageRemove.Remove(ImageId);
            }
            
            return RedirectToAction("Index", "Property" , new { PropertyId = PropertyId });
        }

    }
}       