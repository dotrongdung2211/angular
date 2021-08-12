using System;
using System.Collections.Generic;
using System.Text;

namespace hrmSolution.Data.Entities
{
    public class Tbl_DanhMucTenQuyen
    {
        public Guid Id { set; get; }
        public string C_Code { get; set; }
        public string C_TenQuyen { get; set; }
        public DateTime C_NgayTao { get; set; }
        public int C_TrangThai { get; set; }
        public int C_IsDeleted { get; set; }

        public virtual List<Tbl_NguoiDung> Tbl_NguoiDungs { get; set; }
        public virtual List<Tbl_ChiTietQuyenTheoManHinh> Tbl_ChiTietQuyenTheoManHinhs { get; set; }
    }
}
