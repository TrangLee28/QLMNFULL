//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolManager.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DonThuoc
    {
        public int Id { get; set; }
        public int Id_HS { get; set; }
        public string Id_PH { get; set; }
        public string Ten { get; set; }
        public string HamLuong { get; set; }
        public int SoLuong { get; set; }
        public string DonVi { get; set; }
        public string CachDung { get; set; }
        public bool TrangThai { get; set; }
    
        public virtual HocSinh HocSinh { get; set; }
        public virtual PhuHuynh PhuHuynh { get; set; }
    }
}
