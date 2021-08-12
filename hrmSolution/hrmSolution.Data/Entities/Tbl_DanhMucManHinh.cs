using System;
using System.Collections.Generic;
using System.Text;

namespace hrmSolution.Data.Entities
{
    public class Tbl_DanhMucManHinh
    {
        public Guid Id { set; get; }
        public string C_Code { get; set; }
        public string C_TenManHinh { get; set; }
        public DateTime C_NgayTao { get; set; }
        public int C_TrangThai { get; set; }
        public int C_IsDeleted { get; set; }

        public virtual List<Tbl_ChiTietQuyenTheoManHinh> Tbl_ChiTietQuyenTheoManHinhs { get; set; }
    }
}
