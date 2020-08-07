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
    [Authorize(Roles = "Admin, Quản trị viên, Giáo viên, Phụ huynh")]
    public class DonThuocController : Controller
    {
        TruongMamNonEntities db;
        public DonThuocController()
        {
            db = new TruongMamNonEntities();
        }
        // GET: DonThuoc
        [Authorize(Roles = "Phụ huynh")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin, Quản trị viên, Giáo viên")]
        public ActionResult GiaoVien()
        {
            return View();
        }
        public ActionResult HienDonThuoc(int Id)
        {
            DonThuoc dv = db.DonThuocs.Find(Id);
            if (dv != null)
            {
                dv.TrangThai = false;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult ThemDonThuoc(DonThuocModel model)
        {
            DonThuoc dv = new DonThuoc();
            dv.Ten = model.Ten;
            dv.HamLuong = model.HamLuong;
            dv.SoLuong = model.SoLuong;
            dv.CachDung = model.CachDung;
            dv.DonVi = model.DonVi;
            dv.Id_HS = model.Id_HS;
            dv.Id_PH = model.Id_PH;
            dv.TrangThai = true;
            db.DonThuocs.Add(dv);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult XoaDonThuoc(int Id)
        {
            DonThuoc dv = db.DonThuocs.Find(Id);
            if (dv != null)
            {
                db.DonThuocs.Remove(dv);
                db.SaveChanges();
                return Json(new { Success = "true" });
            }

            return Json(new { Success = "false" });
        }
        public ActionResult SuaDonThuoc(DonThuocModel model, HttpPostedFileBase file)
        {
            DonThuoc dv = db.DonThuocs.Find(model.Id);
            if (dv != null)
            {
                dv.Ten = model.Ten;
                dv.HamLuong = model.HamLuong;
                dv.SoLuong = model.SoLuong;
                dv.CachDung = model.CachDung;
                dv.DonVi = model.DonVi;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}