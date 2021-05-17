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
    public class UserController : Controller
    {
        private Core _db = new Core();
        // GET: Admin/User
        public ActionResult Index(string name = "", string link = "")
        {
            ViewBag.name = name;
            ViewBag.link = link;
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TblImage user, HttpPostedFileBase MainImage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblImage addUser = new TblImage();
                    if (MainImage != null && MainImage.IsImage())
                    {
                        addUser.ImageUrl = Guid.NewGuid().ToString() + Path.GetExtension(MainImage.FileName);
                        MainImage.SaveAs(Server.MapPath("/Resources/Images/" + addUser.ImageUrl));
                        ImageResizer img = new ImageResizer();
                        img.Resize(Server.MapPath("/Resources/Images/" + addUser.ImageUrl),
                            Server.MapPath("/Resources/Images/Thumb/" + addUser.ImageUrl));
                    }
                    else
                    {
                        ModelState.AddModelError("ImageUrl", "عکس آپلود کنید");
                        return View(user);

                    }
                    addUser.Text = user.Text;
                    addUser.Link = user.Link;
                    addUser.Status = user.Status;
                    _db.Image.Add(addUser);
                    _db.Save();
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch
            {
                return View(user);

            }
        }
        public ActionResult Delete(int id)
        {
            TblImage selectUserById = _db.Image.GetById(id);
            bool delete = _db.Image.Delete(selectUserById);
            if (delete)
            {
                _db.Save();
                return Json(new { success = true, responseText = " حذف شد" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, responseText = "خطا در حذف   لطفا بررسی فرمایید" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(int id)
        {
            return View(_db.Image.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TblImage user, HttpPostedFileBase MainImage)
        {
            if (ModelState.IsValid)
            {
                TblImage addUser = _db.Image.GetById(user.ImageId);

                if (MainImage != null && MainImage.IsImage())
                {
                    if (addUser.ImageUrl != null)
                    {
                        string fullPathLogo = Request.MapPath("/Resources/Images/" + addUser.ImageUrl);
                        if (System.IO.File.Exists(fullPathLogo))
                        {
                            System.IO.File.Delete(fullPathLogo);
                        }
                        string fullPathLogo2 = Request.MapPath("/Resources/Images/Thumb/" + addUser.ImageUrl);
                        if (System.IO.File.Exists(fullPathLogo2))
                        {
                            System.IO.File.Delete(fullPathLogo2);
                        }
                    }
                    addUser.ImageUrl = Guid.NewGuid().ToString() + Path.GetExtension(MainImage.FileName);
                    MainImage.SaveAs(Server.MapPath("/Resources/Images/" + addUser.ImageUrl));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Resources/Images/" + addUser.ImageUrl),
                        Server.MapPath("/Resources/Images/Thumb/" + addUser.ImageUrl));
                }
                addUser.Text = user.Text;
                addUser.Link = user.Link;
                addUser.Status = user.Status;
                _db.Image.Update(addUser);
                _db.Save();
                return RedirectToAction("Index");
            }
            return View(user);
        }
        public ActionResult ListUser(string name = "", string link = "")
        {
            List<TblImage> list = new List<TblImage>();
            list.AddRange(_db.Image.Get());
            if (name != "")
            {
                list = list.Where(p => p.Text.Contains(name)).ToList();
            }
            if (link != "")
            {
                list = list.Where(p => p.Link.Contains(link)).ToList();
            }
            return PartialView(list.OrderByDescending(i => i.ImageId));
        }

    }
}