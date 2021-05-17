using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using AzarKandidaV2.Utilities;
using System.IO;
using AzarKandidaV2.Utilities;
using AzarKandidaV2.ViewModels;

namespace AzarKandidaV2.Controllers
{
    public class HomeController : Controller
    {
        private Core _db = new Core();


        public ActionResult Index()
        {
            var res = _db.Image.Get().ToList();
            res.ShuffleList();
            var blog = _db.Blog.Get().ToList();
            res.ShuffleList();
            ImageAndNewsVm list = new ImageAndNewsVm()
            {
                Images = res,
                Blogs= blog
            };
            return View(list);
        }
    }
}