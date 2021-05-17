using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AzarKandidaV2.Controllers
{
    public class BlogController : Controller
    {
        private Core _db = new Core();
        // GET: Blog
        [Route("Blog/{id?}/{name?}")]
        public ActionResult Index(int? id, string name)
        {
            return View(_db.Blog.GetById(id));
        }
    }
}