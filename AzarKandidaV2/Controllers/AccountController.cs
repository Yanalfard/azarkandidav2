using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AzarKandidaV2.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            string name = "SYGsahandi";
            string pass = "136913701379";
            if (name == username && pass == password)
            {
                bool RememberMe = true;
                FormsAuthentication.SetAuthCookie(username, RememberMe);
                return Redirect("/Admin/Home");
            }
            return View();
        }

        [Route("LogOff")]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}