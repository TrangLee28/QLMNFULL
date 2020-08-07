using IdentitySample.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SchoolManager.Models;

namespace SchoolManager.Controllers
{
    [Authorize(Roles = "Admin, Quản trị viên, Giáo viên,Dinh dưỡng,Văn phòng")]
    public class AdminController : Controller
    {
        TruongMamNonEntities db;
        public AdminController()
        {
            db = new TruongMamNonEntities();
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Hoc Sinh
        /// </summary>
        /// <returns></returns>
        
        public ActionResult HocSinh()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddHocSinh(HocSinhModel hocsinh, HttpPostedFileBase file)
        {
            HocSinh hs = new HocSinh();
            hs.BiDanh = hocsinh.BiDanh;
            hs.DanToc = hocsinh.DanToc;
            hs.GioiTinh = hocsinh.GioiTinh;
            hs.HoTen = hocsinh.HoTen;
            hs.NgaySinh = hocsinh.NgaySinh;
            hs.NgayTao = DateTime.Now;
                string subPath = "~/Content/UserUpload/img/hocsinh/";

                bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                try
                {
                    if (file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath(subPath), fileName);
                        file.SaveAs(path);
                        hs.img = fileName;
                        db.HocSinhs.Add(hs);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("HocSinh");
                }
                return RedirectToAction("HocSinh");
            
        }
    }
}