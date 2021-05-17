using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using AzarKandidaV2.Utilities;
using System.IO;

namespace AzarKandidaV2.Areas.Admin.Controllers
{
    public class BlogController : Controller
    {
        private Core _db = new Core();
        // GET: Admin/Blog
        public ActionResult Index(string name = "")
        {
            ViewBag.name = name;
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TblBlog Blog, HttpPostedFileBase MainImage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblBlog addBlog = new TblBlog();
                    if (MainImage != null && MainImage.IsImage())
                    {
                        addBlog.MainImage = Guid.NewGuid().ToString() + Path.GetExtension(MainImage.FileName);
                        MainImage.SaveAs(Server.MapPath("/Resources/Blogs/" + addBlog.MainImage));
                        ImageResizer img = new ImageResizer();
                        img.Resize(Server.MapPath("/Resources/Blogs/" + addBlog.MainImage),
                            Server.MapPath("/Resources/Blogs/Thumb/" + addBlog.MainImage));
                    }
                    else
                    {
                        ModelState.AddModelError("MainImage", "عکس آپلود کنید");
                        return View(Blog);
                    }
                    addBlog.Name = Blog.Name;
                    addBlog.Title = Blog.Title;
                    addBlog.Description = Blog.Description;
                    addBlog.Body = Blog.Body;
                    _db.Blog.Add(addBlog);
                    _db.Save();
                    return RedirectToAction("Index");

                }
                return View(Blog);
            }
            catch
            {
                return View(Blog);

            }
        }
        public ActionResult Delete(int id)
        {
            TblBlog selectBlogById = _db.Blog.GetById(id);
            bool delete = _db.Blog.Delete(selectBlogById);
            if (delete)
            {
                _db.Save();
                return Json(new { success = true, responseText = " حذف شد" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, responseText = "خطا در حذف   لطفا بررسی فرمایید" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(int id)
        {
            return View(_db.Blog.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TblBlog Blog, HttpPostedFileBase MainImage)
        {
            if (ModelState.IsValid)
            {
                TblBlog addBlog = _db.Blog.GetById(Blog.BlogId);

                if (MainImage != null && MainImage.IsImage())
                {
                    if (addBlog.MainImage != null)
                    {
                        string fullPathLogo = Request.MapPath("/Resources/Blogs/" + addBlog.MainImage);
                        if (System.IO.File.Exists(fullPathLogo))
                        {
                            System.IO.File.Delete(fullPathLogo);
                        }
                        string fullPathLogo2 = Request.MapPath("/Resources/Blogs/Thumb/" + addBlog.MainImage);
                        if (System.IO.File.Exists(fullPathLogo2))
                        {
                            System.IO.File.Delete(fullPathLogo2);
                        }
                    }
                    addBlog.MainImage = Guid.NewGuid().ToString() + Path.GetExtension(MainImage.FileName);
                    MainImage.SaveAs(Server.MapPath("/Resources/Blogs/" + addBlog.MainImage));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Resources/Blogs/" + addBlog.MainImage),
                        Server.MapPath("/Resources/Blogs/Thumb/" + addBlog.MainImage));
                }
                addBlog.Name = Blog.Name;
                addBlog.Title = Blog.Title;
                addBlog.Description = Blog.Description;
                addBlog.Body = Blog.Body;
                _db.Blog.Update(addBlog);
                _db.Save();
                return RedirectToAction("Index");
            }
            return View(Blog);
        }
        public ActionResult ListBlog(string name = "")
        {
            List<TblBlog> list = new List<TblBlog>();
            list.AddRange(_db.Blog.Get());
            if (name != "")
            {
                list = list.Where(p => p.Name.Contains(name) || p.Title.Contains(name) || p.Description.Contains(name)).ToList();
            }
            return PartialView(list.OrderByDescending(i => i.BlogId));
        }
    }
}