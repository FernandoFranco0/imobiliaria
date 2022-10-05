using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using RealState.Models;

namespace RealState.Controllers
{
    public class TesteController : Controller
    {

        public ActionResult Teste()
        {

            List<string> teste = new List<string>();
            teste.Add("https://images.unsplash.com/photo-1517926112623-f32a800790d4?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80");
            teste.Add("https://images.unsplash.com/photo-1504507926084-34cf0b939964?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80");
            teste.Add("https://images.unsplash.com/photo-1517926112623-f32a800790d4?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80");

            var a = new PropertyModel
            {
                Id = 1,
                BedroomNumber = 23,
                State = "alaska",
                City = "yes",
                NeighboorHood = "no",
                StreetName = "maybe",
                HouseNumber = 1,
                Area = 100,
                UserId = 10,
                Price = 2000,
                GarageSpace = 100,
                ImagesUrl = teste
            };





            return View(a);
        }

        // GET: Teste
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            var Context = Request.GetOwinContext();
            var AuthenticationManager = Context.Authentication;

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(TesteModel ViewModel) // , string returnUrl
        {
            var Response = new TesteModel();
            Response.Login(ViewModel.Name, ViewModel.Id);

            // Sucesso
            if (Response.IsAuthenticated)
            {
                ClaimsIdentity Identity = new ClaimsIdentity(
                new[] {
                            // UserId
                            new Claim(ClaimTypes.NameIdentifier, ViewModel.Id.ToString(), ClaimValueTypes.Integer),
                            // Email
                            new Claim(ClaimTypes.Email, "a@gmail.com", ClaimValueTypes.Email),
                            // Nome
                            new Claim(ClaimTypes.Name, ViewModel.Name, ClaimValueTypes.String),
                            //// Perfil de acesso
                            //new Claim(ClaimTypes.Role, Response.AccessProfileId.ToString())
                }, DefaultAuthenticationTypes.ApplicationCookie);

                var Context = Request.GetOwinContext();
                var AuthenticationManager = Context.Authentication;

                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

                AuthenticationManager.SignIn(Identity);

                // pq n funciona aqui ?
                //ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
                //int a = 10;
                //IEnumerable<Claim> claims = identity.Claims;
                //var testeclaim = claims.First();
                //a = 20;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                //ModelState.AddModelError(nameof(ViewModel.Password), "E-mail e/ou senha incorretos.");
            }
            return RedirectToAction("Index", "Teste");
            //return Redirect(GetRedirectUrl(returnUrl));
        }

        //private string GetRedirectUrl(string ReturnUrl)
        //{
        //    if (string.IsNullOrEmpty(ReturnUrl) || !Url.IsLocalUrl(ReturnUrl))
        //    {
        //        return Url.Action(nameof(HomeController.Index), HomeController.ControllerName);
        //    }

        //    return ReturnUrl;
        //}


    }
}