using IdentitySample.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using ClosedXML.Excel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SchoolManager.Models;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data;

namespace SchoolManager.Controllers
{
    [Authorize(Roles = "Admin, Quản trị viên, Giáo viên,Văn phòng")]
    public class VanPhongController : Controller
    {
        TruongMamNonEntities db;
        public VanPhongController()
        {
            db = new TruongMamNonEntities();
        }

        [HttpGet]
        public ActionResult HocSinh()
        {
            return View(InitData());
        }
        [HttpGet]
        public ActionResult GiaoVien()
        {
            return View();
        }
        public ActionResult ThoiKhoaBieu()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult LopHoc()
        {
            return View();
        }

        public ActionResult ThemNhomTuoi(string TenNhom)
        {
            if (db.NhomTuois.Where(x => x.TenNhom == TenNhom).Count() > 0)
            {
                ViewBag.er = "Tên nhóm đã tồn tại";
                return View();
            }
            NhomTuoi nhom = new NhomTuoi();

            nhom.TenNhom = TenNhom;
            db.NhomTuois.Add(nhom);
            db.SaveChanges();
            return RedirectToAction("LopHoc");
        }
        [HttpPost]
        public ActionResult LopHoc(string lop, int MaNhomTuoi)
        {
            if (db.Lops.Where(x => x.Lop1 == lop).Count() > 0)
            {
                ViewBag.er = "Tên lớp đã tồn tại";
                return View();
            }
            NhomTuoi nhom = db.NhomTuois.Find(MaNhomTuoi);
            if (nhom != null)
            {
                Lop l = new Lop();
                l.Lop1 = lop;
                l.MaNhom = MaNhomTuoi;
                db.Lops.Add(l);
                db.SaveChanges();
            }

            return View();
        }
        [HttpPost]
        public ActionResult EditLopHoc(int Id, string tenlop, string gvcn)
        {
            Lop l = db.Lops.Where(x => x.Id == Id).FirstOrDefault();
            if (l != null)
            {
                l.Lop1 = tenlop;
                if (l.GiaoVien_Lops.Where(x => x.TrangThai == "Đang phụ trách").Count() > 0 && l.GiaoVien_Lops.Where(x => x.TrangThai == "Đang phụ trách").FirstOrDefault().GiaoVien.Id
                    != gvcn)
                {
                    l.GiaoVien_Lops.Where(x => x.TrangThai == "Đang phụ trách").FirstOrDefault().TrangThai = "Đã thôi phụ trách";
                }
                else if (l.GiaoVien_Lops.Where(x => x.TrangThai == "Đang phụ trách").Count() > 0 && l.GiaoVien_Lops.Where(x => x.TrangThai == "Đang phụ trách").FirstOrDefault().GiaoVien.Id
                   == gvcn)
                {
                    db.SaveChanges();
                    return RedirectToAction("ChitietLopHoc", new { Id = Id });
                }
                GiaoVien_Lops gvl = new GiaoVien_Lops();
                gvl.Id_GiaoVien = gvcn;
                gvl.TrangThai = "Đang phụ trách";
                gvl.Lop = Id;
                db.GiaoVien_Lops.Add(gvl);
                db.SaveChanges();
                return RedirectToAction("ChitietLopHoc", new { Id = Id });
            }
            return RedirectToAction("LopHoc");
        }

        [HttpGet]
        public ActionResult ChitietLopHoc(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        [HttpGet]
        public ActionResult PhuHuynh()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ChitietHocsinh(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        [HttpGet]
        public ActionResult ChiTietGiaoVien(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        [HttpGet]
        public ActionResult ChitietPhuHuynh(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        [HttpGet]
        public ActionResult DiemDanhGiaoVien()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DiemDanhGiaoVien(string idgiaovien, DateTime thoigian)
        {
            DiemDanhGiaoVien diemdanh = new DiemDanhGiaoVien();
            diemdanh.Id_GiaoVien = idgiaovien;
            diemdanh.ThoiGian = thoigian;
            db.DiemDanhGiaoViens.Add(diemdanh);
            db.SaveChanges();
            return View();
        }
        [HttpPost]
        public ActionResult HocSinh_PhuHuynh(string phuhuynh, int hocsinh, string quanhe)
        {
            PhuHuynh_HocSinh phhs = new PhuHuynh_HocSinh();
            phhs.Id_HocSinh = hocsinh;
            phhs.Id_PhuHuynh = phuhuynh;
            phhs.QuanHe = quanhe;
            db.PhuHuynh_HocSinh.Add(phhs);
            db.SaveChanges();
            return RedirectToAction("ChitietHocsinh", new { Id = hocsinh });
        }

        [HttpPost]
        public ActionResult EditDiemDanhGiaoVien(int id, DateTime thoigian)
        {
            DiemDanhGiaoVien diemdanh = db.DiemDanhGiaoViens.Where(x => x.Id == id).FirstOrDefault();
            diemdanh.ThoiGian = thoigian;
            db.SaveChanges();
            return RedirectToAction("DiemDanhGiaoVien");
        }

        [HttpPost]
        public ActionResult DelDiemDanhGiaoVien(int id)
        {
            try
            {
                DiemDanhGiaoVien diemdanh = db.DiemDanhGiaoViens.Where(x => x.Id == id).FirstOrDefault();
                db.DiemDanhGiaoViens.Remove(diemdanh);
                db.SaveChanges();
                return Json(new { Success = "true" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = "false" });
            }
        }
        [HttpPost]
        [Obsolete]
        public ActionResult API_GetDiemDanhGiaoVien(DateTime thoigian)
        {
            List<ListDiemDanhGiaoVienModel> ds = new List<ListDiemDanhGiaoVienModel>();
            string[] dshs = db.GiaoViens.Select(x => x.Id).ToArray();

            System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
            DateTime dauthang = new DateTime(thoigian.Year, thoigian.Month, 1);
            DateTime cuoithang = dauthang.AddMonths(1).AddDays(-1);

            ViewBag.songay = (cuoithang - dauthang).TotalDays;

            foreach (string id in dshs)
            {
                dauthang = new DateTime(thoigian.Year, thoigian.Month, 1);
                for (int i = 0; i <= ViewBag.songay; i++)
                {
                    ds.Add(GetDiemDanhMotGiaoVien(id, dauthang));
                    dauthang = dauthang.AddDays(1);
                }
            }
            return View(ds);
        }
        [Obsolete]
        ListDiemDanhGiaoVienModel GetDiemDanhMotGiaoVien(string idhs, DateTime ngay)
        {

            ListDiemDanhGiaoVienModel hs = new ListDiemDanhGiaoVienModel();
            GiaoVien h = db.GiaoViens.Where(x => x.Id == idhs).FirstOrDefault();
            hs.HoTen = h.HoTen;
            hs.NgaySinh = h.NgaySinh;
            if (ngay.DayOfWeek == DayOfWeek.Sunday || ngay.DayOfWeek == DayOfWeek.Saturday)
            {
                hs.ThoiGian = ngay;
                hs.TrangThai = "ngày nghỉ";
            }
            else if (ngay.Date > DateTime.Now.Date)
            {
                hs.ThoiGian = ngay;
                hs.TrangThai = "trống";
            }
            else if (db.DiemDanhGiaoViens.Where(x => x.Id_GiaoVien == idhs && EntityFunctions.TruncateTime(x.ThoiGian) == EntityFunctions.TruncateTime(ngay)).Count() > 0)
            {
                List<DiemDanhGiaoVien> d = db.DiemDanhGiaoViens.Where(x => x.Id_GiaoVien == idhs && EntityFunctions.TruncateTime(x.ThoiGian) == EntityFunctions.TruncateTime(ngay)).ToList();
                hs.ThoiGian = d[0].ThoiGian;
                hs.TrangThai = "đi";
            }
            else
            {
                hs.ThoiGian = ngay;
                hs.TrangThai = "vắng";
            }
            return hs;
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
            hs.LopHoc = hocsinh.MaLop;
            string subPath = "~/Content/UserUpload/img/hocsinh/";
            db.HocSinhs.Add(hs);
            db.SaveChanges();
            bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
            try
            {
                if (file!=null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath(subPath), fileName);
                    file.SaveAs(path);
                    hs.img = fileName;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Hocsinh");
            }
            return RedirectToAction("ChitietHocsinh", new { Id = hs.Id });

        }

        [HttpPost]
        public ActionResult EditHocSinh(HocSinhModel hocsinh, HttpPostedFileBase file)
        {
            HocSinh hs = db.HocSinhs.Where(x => x.Id == hocsinh.Id).FirstOrDefault();
            hs.BiDanh = hocsinh.BiDanh;
            hs.DanToc = hocsinh.DanToc;
            hs.GioiTinh = hocsinh.GioiTinh;
            hs.HoTen = hocsinh.HoTen;
            //hs.NgaySinh = hocsinh.NgaySinh;
            hs.HKTT = hocsinh.HKTT;
            string subPath = "~/Content/UserUpload/img/hocsinh/";
            hs.LopHoc = hocsinh.MaLop;
            db.SaveChanges();
            bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
            try
            {
                if (file!=null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath(subPath), fileName);
                    file.SaveAs(path);
                    hs.img = fileName;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("HocSinh");
            }
            return RedirectToAction("HocSinh");

        }
        [HttpPost]
        public List<HocSinh> InitData()
        {
            TruongMamNonEntities db = new TruongMamNonEntities();
            List<HocSinh> dshs = db.HocSinhs.ToList();
            return dshs;
        }
        // GET: ClosedXML
        //public ActionResult HocSinh()
        //{
        //    return View(InitData());
        //}

        public ActionResult ExportExcel()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Họ Tên", typeof(string)),
            new DataColumn("Lớp Học", typeof(string)),
            new DataColumn("Ngày Sinh",typeof(string)),
            new DataColumn("Giới tính",typeof(string))});
            var result = InitData();
            foreach (var item in result)
            {
                dt.Rows.Add(item.HoTen, item.Lop.Lop1, item.NgaySinh.ToString("dd/MM/yyyy"), item.GioiTinh);
            }
            //Exporting to Excel
            //string folderPath = "C:\\Excel\\";
            //if (!Directory.Exists(folderPath))
            //{
            //    Directory.CreateDirectory(folderPath);
            //}
            //Codes for the Closed XML
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "HocSinh");

                //wb.SaveAs(folderPath + "DataGridViewExport.xlsx");
                string myName = Server.UrlEncode("Test" + "_" + DateTime.Now.ToShortDateString() + ".xlsx");
                MemoryStream stream = GetStream(wb);// The method is defined below
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=" + myName);
                Response.ContentType = "application/vnd.ms-excel";
                Response.BinaryWrite(stream.ToArray());
                Response.End();
            }

            return RedirectToAction("Index");
        }



        public MemoryStream GetStream(XLWorkbook excelWorkbook)
        {
            MemoryStream fs = new MemoryStream();
            excelWorkbook.SaveAs(fs);
            fs.Position = 0;
            return fs;
        }
        public ActionResult DelHocSinh(int id)
        {
            try
            {
                HocSinh ds = db.HocSinhs.Where(x => x.Id == id).FirstOrDefault();
                db.HocSinhs.Remove(ds);
                db.SaveChanges();
                return Json(new { Success = "true" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = "false" });
            }
        }
    }
}