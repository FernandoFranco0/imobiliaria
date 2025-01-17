﻿using Microsoft.AspNet.Identity;
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
        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel ViewModel, string returnUrl)
        {
            var Response = new RealStateService.User().Login(ViewModel.Email, ViewModel.Password);

            // Sucesso
            if (Response.IsAuthenticated)
            {
                ClaimsIdentity Identity = new ClaimsIdentity(
                new[] {
                            // UserId
                            new Claim(ClaimTypes.NameIdentifier, Response.Id.ToString() , ClaimValueTypes.Integer),
                            // Email
                            new Claim(ClaimTypes.Email, Response.Email, ClaimValueTypes.Email),
                            // Nome
                            new Claim(ClaimTypes.Name, Response.Name, ClaimValueTypes.String),
                            // Perfil de acesso
                            new Claim(ClaimTypes.Role, Response.RoleId.ToString())
                }, DefaultAuthenticationTypes.ApplicationCookie);

                var Context = Request.GetOwinContext();
                var AuthenticationManager = Context.Authentication;

                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

                AuthenticationManager.SignIn(Identity);

                return Redirect(GetRedirectUrl(returnUrl));
            }
            else
            {
                ModelState.AddModelError("Password", "E-mail e/ou senha incorretos.");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(ViewModel);
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

            var LoggedUser = OtherUser.Get(User.Email);
            if (LoggedUser.Id != -1)
            {
                ModelState.AddModelError("Email", "E-mail já registrado");
                return View(User);
            }

            OtherUser.Add(User);

            ClaimsIdentity Identity = new ClaimsIdentity(
                new[] {
                            // UserId
                            new Claim(ClaimTypes.NameIdentifier, LoggedUser.Id.ToString() , ClaimValueTypes.Integer),
                            // Email
                            new Claim(ClaimTypes.Email, User.Email, ClaimValueTypes.Email),
                            // Nome
                            new Claim(ClaimTypes.Name, User.Name, ClaimValueTypes.String),
                            // Perfil de acesso
                            new Claim(ClaimTypes.Role, LoggedUser.RoleId.ToString())
                }, DefaultAuthenticationTypes.ApplicationCookie);

            var Context = Request.GetOwinContext();
            var AuthenticationManager = Context.Authentication;

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignIn(Identity);


            return RedirectToAction("Index", "Home");
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

        [Authorize]
        public ActionResult MyFavorites()
        {
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            var claims = identity.Claims.ToList();
            var Userid = Int32.Parse(claims[0].Value);

            var FavoriteHelper = new RealStateService.Favorite();

            var MyFavoritesList = FavoriteHelper.List(Userid);

            return View(MyFavoritesList);
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