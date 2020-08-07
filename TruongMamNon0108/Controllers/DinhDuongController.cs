using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SchoolManager.Models;

namespace SchoolManager.Controllers
{
    [Authorize(Roles = "Admin, Quản trị viên, Văn phòng, Dinh dưỡng")]
    public class DinhDuongController : Controller
    {
        TruongMamNonEntities db;
        public DinhDuongController()
        {
            db = new TruongMamNonEntities();
        }
        // GET: ThucDon
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HienThucDon(int Id)
        {
            ThucDon dv = db.ThucDons.Find(Id);
            if (dv != null)
            {
                dv.TrangThai = !dv.TrangThai;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult ChiTiet(int Id)
        {
            ThucDon dv = db.ThucDons.Find(Id);
            if (dv != null)
            {
                ThucDonModel model = new ThucDonModel();
                model.Id = dv.Id;
                model.TenThucDon = dv.TenThucDon;
                model.MaNhom = (int)dv.MaNhom;
                model.MoTa = dv.MoTa;
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult SuaThucDon(ThucDonModel model, HttpPostedFileBase file)
        {
            ThucDon dv = db.ThucDons.Find(model.Id);
            if (dv != null)
            {
                dv.TenThucDon = model.TenThucDon;
                dv.MoTa = model.MoTa;
                dv.MaNhom = model.MaNhom;
                db.SaveChanges();
            }

            string subPath = "~/Content/UserUpload/img/ThucDon/"; // your code goes here

            bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath(subPath), dv.Id.ToString() + ".png");
                    file.SaveAs(path);
                }
            }
            catch (Exception ex)
            {
                //return Json(new { Success = "false", Error = ex.Message });
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        public ActionResult XoaThucDon(int Id)
        {
            ThucDon dv = db.ThucDons.Find(Id);
            if (dv != null)
            {
                db.ThucDons.Remove(dv);
                db.SaveChanges();
                return Json(new { Success = "true" });
            }

            return Json(new { Success = "false" });
        }
        public ActionResult ThemThucDon(ThucDonModel model, HttpPostedFileBase file)
        {
            ThucDon dv = new ThucDon();
            dv.TenThucDon = model.TenThucDon;
            dv.MoTa = model.MoTa;
            dv.TrangThai = true;
            dv.MaNhom = model.MaNhom;
            db.ThucDons.Add(dv);
            db.SaveChanges();
            string subPath = "~/Content/UserUpload/img/ThucDon/"; // your code goes here

            bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

            try
            {
                if (file.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath(subPath), dv.Id.ToString() + ".png");
                    file.SaveAs(path);
                }
            }
            catch (Exception ex)
            {
                //return Json(new { Success = "false", Error = ex.Message });
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}