using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using work_01.Models;
using work_01.Models.ViewModel;

namespace work_01.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterVM rvm) 
        {
            if (ModelState.IsValid)
            {
                var userStore = new UserStore<IdentityUser>();
                var userManager = new UserManager<IdentityUser>(userStore);
                var user = new IdentityUser { UserName = rvm.UserName };
                var result = userManager.Create(user, rvm.Password);
                Registration r = new Registration 
                {
                    UserName=rvm.UserName,
                    Password=rvm.Password,
                    ConfirmPassword=rvm.ConfirmPassword
                };
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Accounts");
                }
                else
                {
                    ModelState.AddModelError("", "Register Failed!!");
                    return View(rvm);
                }
            }
            return View(rvm);
        }

        public ActionResult Login() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM lvm)
        {
            if (ModelState.IsValid)
            {
                var userStore = new UserStore<IdentityUser>();
                var userManager = new UserManager<IdentityUser>(userStore);
                var user = userManager.Find(lvm.UserName, lvm.Password);
                if (user != null)
                {
                    var authManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
                    authManager.SignIn(new AuthenticationProperties() { }, userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login Failed!!");
                    return View(lvm);
                }
            }
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            var authManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Login", "Accounts");
        }
    }
}