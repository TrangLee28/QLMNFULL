using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SchoolManager.Models;
namespace SchoolManager.Controllers
{
    public class HomeController : Controller
    {
        TruongMamNonEntities db;
        AspNetUser me;
        public HomeController()
        {
            db = new TruongMamNonEntities();
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        [HttpGet]
        public ActionResult ThucDon()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DichVu()
        {
            return View();
        }
        public ActionResult ChiTietThucDon(int Id)
        {
            ThucDon dv = db.ThucDons.Find(Id);
            if (dv != null)
            {
                return View(dv);
            }
            else return RedirectToAction("ThucDon");
        }

        public ActionResult ChiTietDichVu(int Id)
        {
            DichVu dv = db.DichVus.Find(Id);
            if (dv != null)
            {
                return View(dv);
            }
            else return RedirectToAction("DichVu");
        }
        [Authorize(Roles = "Phụ huynh")]
        public ActionResult DangKyDichVu(int Id)
        {
            string getid = User.Identity.GetUserId();
            me = db.AspNetUsers.Find(getid);
            DichVu dv = db.DichVus.Find(Id);
            if (dv != null && me.PhuHuynh.PhuHuynh_HocSinh.Count() > 0)
            {
                string maph = User.Identity.GetUserId();
                if (db.DangKyDichVus.Where(x=>x.MaPhuHuynh== maph && x.DichVu.Id==dv.Id).Count()==0)
                {
                    DangKyDichVu dk = new DangKyDichVu();
                    dk.DichVu = dv;
                    dk.MaPhuHuynh = maph;
                    dk.ThoiGianDK = DateTime.Now;
                    db.DangKyDichVus.Add(dk);
                        foreach (var item in me.PhuHuynh.PhuHuynh_HocSinh.ToList())
                        {
                        ChiTietDangKyDichVu chi = new ChiTietDangKyDichVu();
                        chi.MaDangKy = dk.Id;
                        chi.MaHocSinh = (int)item.Id_HocSinh;
                        db.ChiTietDangKyDichVus.Add(chi);
                    }
                    db.SaveChanges();
                    ViewBag.Mess = "Chúc mừng, bạn đã đăng ký thành công. Mã hóa đơn của bạn là: <b>" + dk.Id + "</b>";
                }
                else
                {
                    ViewBag.Mess = "Bạn đã đăng ký dịch vụ này trước đó rồi, vui lòng kiểm tra lại!";
                }
                if (me.PhuHuynh.PhuHuynh_HocSinh.Count() == 0) {
                    ViewBag.Mess = "Bạn không có học sinh nào để áp dụng dịch vụ!";
                }
                    return View("~/Views/Home/Message.cshtml");
            }
            else return RedirectToAction("DichVu");
        }
        [Authorize(Roles = "Phụ huynh")]
        public ActionResult HuyDKDichVu(int Id)
        {
            DangKyDichVu dv = db.DangKyDichVus.Find(Id);
            if (dv != null)
            {
                string maph = User.Identity.GetUserId();
                if (dv.MaPhuHuynh== maph && !dv.TrangThai)
                {
                    db.DangKyDichVus.Remove(dv);
                    db.SaveChanges();
                    ViewBag.Mess = "Chúc mừng, bạn đã hủy đăng ký dịch vụ thành công!";
                }
                else
                {
                    ViewBag.Mess = "Không thể hủy đăng ký do đã thanh toán hoặc chưa đăng ký trước đó, vui lòng kiểm tra lại!";
                }

                return View("~/Views/Home/Message.cshtml");
            }
            else return RedirectToAction("Index", "Manage");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DiemDanhHocSinh()
        {

            return View();
        }
    }
}
