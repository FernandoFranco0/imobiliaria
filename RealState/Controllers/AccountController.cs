using Microsoft.AspNet.Identity;
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
    public class AccountController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string ReturnUrl )
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UserModel ViewModel , string returnUrl)
        {
            var Response = new RealStateService.User();
            var LoginUser = Response.Get(ViewModel.Email);

            // Sucesso
            if (Response.Login(ViewModel))
            {
                ClaimsIdentity Identity = new ClaimsIdentity(
                new[] {
                            // UserId
                            new Claim(ClaimTypes.NameIdentifier, LoginUser.Id.ToString() , ClaimValueTypes.Integer),
                            // Email
                            new Claim(ClaimTypes.Email, ViewModel.Email, ClaimValueTypes.Email),
                            // Nome
                            new Claim(ClaimTypes.Name, LoginUser.Name, ClaimValueTypes.String),
                            // Perfil de acesso
                            new Claim(ClaimTypes.Role, LoginUser.Role.ToString())
                }, DefaultAuthenticationTypes.ApplicationCookie);

                var Context = Request.GetOwinContext();
                var AuthenticationManager = Context.Authentication;

                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

                AuthenticationManager.SignIn(Identity);

                return Redirect(GetRedirectUrl(returnUrl));
            }
            else
            {
                //ModelState.AddModelError(nameof(ViewModel.Password), "E-mail e/ou senha incorretos.");
            }

            return RedirectToAction("Login", "Account" , new { returnUrl = returnUrl});
        }

        public ActionResult Logout()
        {
            var Context = Request.GetOwinContext();
            var AuthenticationManager = Context.Authentication;

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {         
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(UserModel User)
        {
            var OtherUser = new RealStateService.User();
            OtherUser.Add(User);

            var Response = new RealStateService.User();
            var LoggedUser = Response.Get(User.Email);

            ClaimsIdentity Identity = new ClaimsIdentity(
                new[] {
                            // UserId
                            new Claim(ClaimTypes.NameIdentifier, LoggedUser.Id.ToString() , ClaimValueTypes.Integer),
                            // Email
                            new Claim(ClaimTypes.Email, User.Email, ClaimValueTypes.Email),
                            // Nome
                            new Claim(ClaimTypes.Name, User.Name, ClaimValueTypes.String),
                            // Perfil de acesso
                            new Claim(ClaimTypes.Role, LoggedUser.Role.ToString())
                }, DefaultAuthenticationTypes.ApplicationCookie);

            var Context = Request.GetOwinContext();
            var AuthenticationManager = Context.Authentication;

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignIn(Identity);


            return RedirectToAction("Index" , "Home");
        }

        [Authorize]
        public ActionResult MyInfo()
        {
            var UserService = new RealStateService.User();

            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            var Userid = Int32.Parse(claims.First().Value);

            return View(UserService.Get(Userid));
        }

        private string GetRedirectUrl(string ReturnUrl)
        {
            if (string.IsNullOrEmpty(ReturnUrl) || !Url.IsLocalUrl(ReturnUrl))
            {
                return Url.Action(nameof(HomeController.Index), "Home");
            }

            return ReturnUrl;
        }

    }
}