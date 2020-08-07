using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManager.Models
{
    public class HocSinhModel
    {
        public int Id { get; set; }
        public string HoTen { get; set; }
        public string BiDanh { get; set; }
        public string DanToc { get; set; }
        public string HKTT { get; set; }
        public System.DateTime NgaySinh { get; set; }
        public string img { get; set; }
        public System.DateTime NgayTao { get; set; }
        public string GioiTinh { get; set; }

        public int MaLop { get; set; }
        
    }

    public class DichVuModel
    {
        public int Id { get; set; }
        [Required]
        public string TenDichVu { get; set; }
        [Required]
        public int MaLoaiDV { get; set; }
        public Double Gia { get; set; }


        public bool TrangThai { get; set; }

        //public System.DateTime ThoiHan { get; set; }

        [Required]
        [AllowHtml]
        [UIHint("tinymce_jquery_full")]
        public string MoTa { get; set; }
    }
    public class ThucDonModel
    {
        public int Id { get; set; }
        [Required]
        public string TenThucDon{ get; set; }
        [Required]
        public int MaNhom { get; set; }
        public bool TrangThai { get; set; }


        [Required]
        [AllowHtml]
        [UIHint("tinymce_jquery_full")]
        public string MoTa { get; set; }
    }

    public class DonThuocModel
    {
        public int Id { get; set; }
        [Required]
        public string Ten { get; set; }
        [Required]
        public string Id_PH { get; set; }
        [Required]
        public int Id_HS { get; set; }
        public bool TrangThai { get; set; }

        public string HamLuong { get; set; }
        public int SoLuong { get; set; }
        public string CachDung { get; set; }
        public string DonVi { get; set; }
    }

    public class ListDiemDanhHocSinhModel
    {
        public string HoTen { get; set; }
        public int Lop { get; set; }
        public System.DateTime ThoiGian { get; set; }
        public string TrangThaiDi { get; set; }
        public string TrangThaiVe { get; set; }
    }
    public class ListDiemDanhGiaoVienModel
    {
        public string HoTen { get; set; }

        public DateTime NgaySinh { get; set; }
        public System.DateTime ThoiGian { get; set; }
        public string TrangThai { get; set; }
    }
}