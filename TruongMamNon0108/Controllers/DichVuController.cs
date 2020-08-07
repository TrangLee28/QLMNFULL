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
    [Authorize(Roles = "Admin, Quản trị viên, Văn phòng")]
    public class DichVuController : Controller
    {
        TruongMamNonEntities db;
        public DichVuController()
        {
            db = new TruongMamNonEntities();
        }
        // GET: DichVu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HienDichVu(int Id)
        {
            DichVu dv = db.DichVus.Find(Id);
            if (dv != null)
            {
                dv.TrangThai = false;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult ThemLoaiDichVu(string Ten, string MoTa)
        {
            LoaiDV dv = new LoaiDV();
            dv.TenLoaiDV = Ten;
            dv.MoTa = dv.MoTa;
            db.LoaiDVs.Add(dv);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ThanhToanDichVu(int Id)
        {
            DangKyDichVu dv = db.DangKyDichVus.Find(Id);
            if (dv != null)
            {
                dv.TrangThai = !dv.TrangThai;
                dv.NguoiThu = User.Identity.GetUserId();
                dv.ThoiGianThu = DateTime.Now;
                db.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }
        public ActionResult ChiTiet(int Id)
        {
            DichVu dv = db.DichVus.Find(Id);
            if (dv != null)
            {
                DichVuModel model = new DichVuModel();
                model.Id = dv.Id;
                model.TenDichVu = dv.TenDichVu;
                model.MaLoaiDV = dv.MaLoaiDV;
                model.Gia = dv.Gia;
                model.MoTa = dv.MoTa;
                return View(model);
            }

            return RedirectToAction("Index");
        }
        public ActionResult ThemDichVu(DichVuModel model,int[] MaLop, HttpPostedFileBase file)
        {
            DichVu dv = new DichVu();
            dv.TenDichVu = model.TenDichVu;
            dv.Gia = model.Gia;
            dv.MoTa = model.MoTa;
            dv.TrangThai = true;
            dv.MaLoaiDV = model.MaLoaiDV;
            db.DichVus.Add(dv);
            db.SaveChanges();
            if (MaLop != null)
            {
                
                foreach(var item in MaLop)
                {
                    DichVu_Lop dvl = new DichVu_Lop();
                    dvl.MaDichVu = dv.Id;
                    dvl.MaLop = item;
                    db.DichVu_Lop.Add(dvl);
                }
            }
            
            db.SaveChanges();
            string subPath = "~/Content/UserUpload/img/dichvu/"; // your code goes here

            bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

            try
            {
                if (file!=null && file.ContentLength > 0)
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
        public ActionResult XoaDichVu(int Id)
        {
            DichVu dv = db.DichVus.Find(Id);
            if (dv != null && dv.DangKyDichVus.Count()<1)
            {
                db.DichVus.Remove(dv);
                db.SaveChanges();
                return Json(new { Success = "true" });
            }

            return Json(new { Success = "false" });
        }
        public ActionResult SuaDichVu(DichVuModel model, HttpPostedFileBase file)
        {
            DichVu dv = db.DichVus.Find(model.Id);
            if (dv != null)
            {
                dv.TenDichVu = model.TenDichVu;
                dv.Gia = model.Gia;
                dv.MoTa = model.MoTa;
                dv.MaLoaiDV = model.MaLoaiDV;
                db.SaveChanges();
            }
                
            string subPath = "~/Content/UserUpload/img/dichvu/"; // your code goes here

            bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

            try
            {
                if (file!=null && file.ContentLength > 0)
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