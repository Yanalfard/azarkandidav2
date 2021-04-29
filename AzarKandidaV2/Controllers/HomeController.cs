using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using AzarKandidaV2.Utilities;
using System.IO;

namespace AzarKandidaV2.Controllers
{
    public class HomeController : Controller
    {
        private Core _db = new Core();

        public ActionResult Index()
        {
            return View(_db.Image.Get(orderBy: i => i.OrderByDescending(k => k.ImageId)));
        }
    }
}