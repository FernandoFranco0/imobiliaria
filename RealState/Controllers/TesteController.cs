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

            return View();
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