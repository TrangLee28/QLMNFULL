using IdentitySample.Models;
using Microsoft.AspNet.Identity.Owin;
using ClosedXML.Excel;
using System;
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
using Microsoft.AspNet.Identity;
using System.Data;

namespace SchoolManager.Controllers
{
    public class GiaoVienController : Controller
    {

        TruongMamNonEntities db;
        public GiaoVienController()
        {
            db = new TruongMamNonEntities();
        }
        // GET: GiaoVien
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult DiemDanhHocSinh()
        {

            //return View(InitData());
            return View();
        }

        public ActionResult DiemDanhDuaDiTatCa()
        {
            GiaoVien giaoVien = db.GiaoViens.Find(User.Identity.GetUserId());
            if (giaoVien != null)
            {

                //Lop l = db.Lops.Find(lop);
                foreach (var item in giaoVien.GiaoVien_Lops.Where(x => x.TrangThai == "Đang phụ trách").ToList())
                {
                    Lop l = item.Lop1;
                    foreach (var item2 in l.HocSinhs.ToList())
                    {
                        if (db.DiemDanhHocSinhs.Where(x => x.Id_HocSinh == item2.Id && x.DenVe == "Đưa đi" && EntityFunctions.TruncateTime(x.ThoiGian) == EntityFunctions.TruncateTime(DateTime.Now)).Count() == 0)
                        {
                            DiemDanhMotHocSinh(item2.Id, DateTime.Now, "", "Đưa đi");
                        }
                    }



                }
            }

            return RedirectToAction("DiemDanhHocSinh");
        }
        public ActionResult DiemDanhDonVeTatCa()
        {
            GiaoVien giaoVien = db.GiaoViens.Find(User.Identity.GetUserId());
            if (giaoVien != null)
            {

                //Lop l = db.Lops.Find(lop);
                foreach (var item in giaoVien.GiaoVien_Lops.Where(x => x.TrangThai == "Đang phụ trách").ToList())
                {
                    Lop l = item.Lop1;


                    foreach (var item2 in l.HocSinhs.ToList())
                    {
                        if (db.DiemDanhHocSinhs.Where(x => x.Id_HocSinh == item2.Id && x.DenVe == "Đưa đi" && EntityFunctions.TruncateTime(x.ThoiGian) == EntityFunctions.TruncateTime(DateTime.Now)).Count() == 1 && db.DiemDanhHocSinhs.Where(x => x.Id_HocSinh== item2.Id && x.DenVe == "Đón về" && EntityFunctions.TruncateTime(x.ThoiGian) == EntityFunctions.TruncateTime(DateTime.Now)).Count() == 0)
                        {
                            DiemDanhMotHocSinh(item2.Id, DateTime.Now, "", "Đón về");
                        }
                    }



                }
            }
            return RedirectToAction("DiemDanhHocSinh");
        }
        //public List<DiemDanhHocSinh> InitData()
        //{
        //    TruongMamNonEntities db = new TruongMamNonEntities();
        //    List<DiemDanhHocSinh> ddhs = db.DiemDanhHocSinhs.ToList();
        //    return ddhs;
        //}
        //public ActionResult ExportExcel()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Họ Tên", typeof(string)),
        //    new DataColumn("Lớp Học", typeof(string)),
        //    new DataColumn("Ngày Sinh",typeof(string)),
        //    new DataColumn("Giới tính",typeof(string))});
        //    var result = InitData();
        //    foreach (var item in result)
        //    {
        //        dt.Rows.Add(item.HoTen, item.Lop.Lop1, item.NgaySinh.ToString("dd/MM/yyyy"), item.GioiTinh);
        //    }
        //    //Exporting to Excel
        //    //string folderPath = "C:\\Excel\\";
        //    //if (!Directory.Exists(folderPath))
        //    //{
        //    //    Directory.CreateDirectory(folderPath);
        //    //}
        //    //Codes for the Closed XML
        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        wb.Worksheets.Add(dt, "HocSinh");

        //        //wb.SaveAs(folderPath + "DataGridViewExport.xlsx");
        //        string myName = Server.UrlEncode("Test" + "_" + DateTime.Now.ToShortDateString() + ".xlsx");
        //        MemoryStream stream = GetStream(wb);// The method is defined below
        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.AddHeader("content-disposition", "attachment; filename=" + myName);
        //        Response.ContentType = "application/vnd.ms-excel";
        //        Response.BinaryWrite(stream.ToArray());
        //        Response.End();
        //    }

        //    return RedirectToAction("Index");
        //}
        public int DiemDanhMotHocSinh(int HocSinh, DateTime thoigian, string nguoiduadon, string trangthai)
        {
            List<DiemDanhHocSinh> d = db.DiemDanhHocSinhs.Where(x => x.Id_HocSinh == HocSinh && EntityFunctions.TruncateTime(x.ThoiGian) == EntityFunctions.TruncateTime(thoigian)).ToList();
            if (d.Count() > 0)
            {
                foreach (var item in d)
                {
                    if (item.DenVe == trangthai)
                        return 1;
                }
            }
            DiemDanhHocSinh diemdanh = new DiemDanhHocSinh();
            diemdanh.Id_HocSinh = HocSinh;
            diemdanh.ThoiGian = thoigian;
            diemdanh.NguoiDuaDon = nguoiduadon;
            diemdanh.DenVe = trangthai;
            diemdanh.Id_NguoiDiemDanh = User.Identity.GetUserId();
            db.DiemDanhHocSinhs.Add(diemdanh);
            return db.SaveChanges();
        }

        [HttpPost]
        public ActionResult GetDiemDanhHocSinh_Ngay(DateTime thoigian, string trangthai, int? Lop)
        {
            List<DiemDanhHocSinh> d = new List<DiemDanhHocSinh>();
            List<Lop> lops = new List<Lop>();
            List<int> idlops = new List<int>();
            if (User.IsInRole("Giáo viên"))
            {
                string getid = User.Identity.GetUserId();
                GiaoVien giaoVien = db.GiaoViens.Find(getid);
                foreach (var item in giaoVien.GiaoVien_Lops.Where(x => x.TrangThai == "Đang phụ trách").ToList())
                {
                    lops.Add(item.Lop1);
                }
            }
            if (User.IsInRole("Quản trị viên"))
            {
                lops = db.Lops.ToList();
            }
            if (Lop.HasValue)
            {
                if (trangthai != null)
                {
                    d = db.DiemDanhHocSinhs.Where(x => x.HocSinh.LopHoc == Lop && x.DenVe.Trim() == trangthai.Trim() && EntityFunctions.TruncateTime(x.ThoiGian) == EntityFunctions.TruncateTime(thoigian)).ToList();

                }
                else
                {
                    d = db.DiemDanhHocSinhs.Where(x => x.HocSinh.LopHoc == Lop && EntityFunctions.TruncateTime(x.ThoiGian) == EntityFunctions.TruncateTime(thoigian)).ToList();

                }
            }
            else
            {
                idlops = lops.Select(x => x.Id).Distinct().ToList();
                if (trangthai != null)
                {
                    d = db.DiemDanhHocSinhs.Where(x => idlops.Contains(x.HocSinh.LopHoc) && x.DenVe.Trim() == trangthai.Trim() && EntityFunctions.TruncateTime(x.ThoiGian) == EntityFunctions.TruncateTime(thoigian)).ToList();

                }
                else
                {
                    d = db.DiemDanhHocSinhs.Where(x => idlops.Contains(x.HocSinh.LopHoc) && EntityFunctions.TruncateTime(x.ThoiGian) == EntityFunctions.TruncateTime(thoigian)).ToList();

                }
            }


            return View(d);
        }

        [HttpPost]
        public ActionResult DiemDanhHocSinh(int HocSinh, DateTime thoigian, string nguoiduadon, string trangthai, HttpPostedFileBase file)
        {
            List<DiemDanhHocSinh> d = db.DiemDanhHocSinhs.Where(x => x.Id_HocSinh == HocSinh && EntityFunctions.TruncateTime(x.ThoiGian) == EntityFunctions.TruncateTime(thoigian)).ToList();
            if (d.Count() > 0)
            {
                foreach (var item in d)
                {
                    if (item.DenVe == trangthai)
                        return View();
                }
            }
            DiemDanhHocSinh diemdanh = new DiemDanhHocSinh();
            diemdanh.Id_HocSinh = HocSinh;
            diemdanh.ThoiGian = thoigian;
            diemdanh.NguoiDuaDon = nguoiduadon;
            diemdanh.DenVe = trangthai;
            diemdanh.Id_NguoiDiemDanh = User.Identity.GetUserId();
            db.DiemDanhHocSinhs.Add(diemdanh);
            db.SaveChanges();

            string subPath = "~/Content/UserUpload/img/DiemDanhHocSinh/";
            bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
            try
            {
                if (file!=null && file.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath(subPath), diemdanh.Id.ToString() + ".png");
                    file.SaveAs(path);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("DiemDanhHocSinh");
            }
            return View();
        }

        [HttpPost]
        [Obsolete]
        public ActionResult API_GetDiemDanhHocSinh_Lop(int Lop, DateTime thoigian)
        {
            List<ListDiemDanhHocSinhModel> ds = new List<ListDiemDanhHocSinhModel>();
            int[] dshs = db.HocSinhs.Where(x => x.LopHoc == Lop).Select(x => x.Id).ToArray();

            System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
            DateTime dauthang = new DateTime(thoigian.Year, thoigian.Month, 1);
            DateTime cuoithang = dauthang.AddMonths(1).AddDays(-1);

            ViewBag.songay = (cuoithang - dauthang).TotalDays;

            foreach (int id in dshs)
            {
                dauthang = new DateTime(thoigian.Year, thoigian.Month, 1);
                for (int i = 0; i <= ViewBag.songay; i++)
                {
                    ds.Add(GetDiemDanhMotHocSinh(id, dauthang));
                    dauthang = dauthang.AddDays(1);
                }
            }
            return View(ds);
        }
       
        [Obsolete]
        ListDiemDanhHocSinhModel GetDiemDanhMotHocSinh(int idhs, DateTime ngay)
        {
            ListDiemDanhHocSinhModel hs = new ListDiemDanhHocSinhModel();
            HocSinh h = db.HocSinhs.Where(x => x.Id == idhs).FirstOrDefault();
            hs.HoTen = h.HoTen;
            hs.Lop = h.LopHoc;
            if (ngay.DayOfWeek == DayOfWeek.Sunday || ngay.DayOfWeek == DayOfWeek.Saturday)
            {
                hs.ThoiGian = ngay;
                hs.TrangThaiDi = hs.TrangThaiVe = "ngày nghỉ";
            }
            else if (ngay.Date > DateTime.Now.Date)
            {
                hs.ThoiGian = ngay;
                hs.TrangThaiDi = hs.TrangThaiVe = "trống";
            }
            else if (db.DiemDanhHocSinhs.Where(x => x.Id_HocSinh == idhs && EntityFunctions.TruncateTime(x.ThoiGian) == EntityFunctions.TruncateTime(ngay)).Count() > 0)
            {
                List<DiemDanhHocSinh> d = db.DiemDanhHocSinhs.Where(x => x.Id_HocSinh == idhs && EntityFunctions.TruncateTime(x.ThoiGian) == EntityFunctions.TruncateTime(ngay)).ToList();
                hs.ThoiGian = d[0].ThoiGian;
                hs.TrangThaiDi = "đi";
                hs.TrangThaiVe = "vắng";
                if (d.Count() > 1)
                {
                    hs.TrangThaiDi = hs.TrangThaiVe = "đi";
                }
            }
            else
            {
                hs.ThoiGian = ngay;
                hs.TrangThaiVe = hs.TrangThaiDi = "vắng";
            }
            return hs;
        }
        [HttpPost]
        public ActionResult EditDiemDanhHocSinh(int id, DateTime thoigian, string nguoiduadon, string trangthai, HttpPostedFileBase file)
        {
            DiemDanhHocSinh diemdanh = db.DiemDanhHocSinhs.Where(x => x.Id == id).FirstOrDefault();
            diemdanh.ThoiGian = thoigian;
            diemdanh.NguoiDuaDon = nguoiduadon;
            diemdanh.DenVe = trangthai;
            db.SaveChanges();

            string subPath = "~/Content/UserUpload/img/DiemDanhHocSinh/";
            bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
            try
            {
                if (file!=null && file.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath(subPath), diemdanh.Id.ToString() + ".png");
                    file.SaveAs(path);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("DiemDanhHocSinh");
            }
            return RedirectToAction("DiemDanhHocSinh");
        }

        [HttpPost]
        public ActionResult DelDiemDanhHocSinh(int id)
        {

            try
            {
                DiemDanhHocSinh diemdanh = db.DiemDanhHocSinhs.Find(id);
                DiemDanhHocSinh diemdanhve = db.DiemDanhHocSinhs.Where(x => x.Id_HocSinh == diemdanh.Id_HocSinh && x.DenVe == "Đón về" && EntityFunctions.TruncateTime(diemdanh.ThoiGian) == EntityFunctions.TruncateTime(DateTime.Now)).FirstOrDefault();
                if (diemdanh != null)
                {
                    db.DiemDanhHocSinhs.Remove(diemdanh);
                    if(diemdanhve!=null)
                    {
                        db.DiemDanhHocSinhs.Remove(diemdanhve);
                    }
                    
                    db.SaveChanges();
                    return Json(new { Success = "true" });
                }
                
                return Json(new { Success = "false" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = "false" });
            }
        }
    }
}