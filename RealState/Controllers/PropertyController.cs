using RealState.Helper;
using RealState.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var Property = new RealStateService.Property();

            return View(Property.Get(PropertyId));
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddUpdate(int? id , int UserId)
        {
            var Property = new RealStateService.Property();

            if (id.HasValue) return View(Property.Get((int)id));

            var NewProperty = new PropertyModel();
            NewProperty.UserId = UserId;

            return View(NewProperty);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Add(PropertyModel Property , List<HttpPostedFileBase> Photos)
        {
            var NewProperty = new RealStateService.Property();

            if(Photos.Count < 1)
            {
                // logica do erro
            }

            int propertyId = NewProperty.Add(Property);

            if(propertyId == -1)
            {
                //logica do erro
            }

            var Image = new RealStateService.Image();

            var NewImage = new ImageModel();
            NewImage.PropertyId = propertyId;

            foreach (var photo in Photos)
            {
                byte[] photoBytes = new byte[photo.ContentLength];
                photo.InputStream.Read(photoBytes, 0, photo.ContentLength);
                NewImage.ByteCodeBase64 = Convert.ToBase64String(photoBytes);
                Image.Add(NewImage);
            }
  

            return RedirectToAction("Index" , "Home");
        }

        public ActionResult Update(PropertyModel Property , List<HttpPostedFileBase> Photos)
        {
            var PropertyService = new RealStateService.Property();
            PropertyService.Update(Property);

            var Image = new RealStateService.Image();

            var NewImage = new ImageModel();
            NewImage.PropertyId = Property.Id;

            foreach (var photo in Photos)
            {
                if(photo != null)
                {
                    byte[] photoBytes = new byte[photo.ContentLength];
                    photo.InputStream.Read(photoBytes, 0, photo.ContentLength);
                    NewImage.ByteCodeBase64 = Convert.ToBase64String(photoBytes);
                    Image.Add(NewImage);
                }
            }

            return RedirectToAction("Index", "Property", new { @PropertyId = Property.Id });
        }


        [Authorize]
        public ActionResult Remove(int id)
        {
            var PropertyRemove = new RealStateService.Property();
            PropertyRemove.Remove(id);
            return RedirectToAction("Index");
        }

        public PartialViewResult GetImagePartialView(List<PropertyModel> Property)
        {
            return PartialView("_Image", Property);
        }
    }
}