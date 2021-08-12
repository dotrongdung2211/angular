using System;
using System.Collections.Generic;
using System.Text;

namespace hrmSolution.Data.Entities
{
    public class Tbl_ChiTietQuyenTheoManHinh
    {
        public Guid Id { set; get; }
        public DateTime C_NgayTao { get; set; }
        public int C_TrangThai { get; set; }
        public int C_IsDeleted { get; set; }

        public Guid Fk_DanhMucTenQuyen { set; get; }
        public virtual Tbl_DanhMucTenQuyen Tbl_DanhMucTenQuyen { get; set; }
        public Guid Fk_DanhMucChucNang { set; get; }
        public virtual Tbl_DanhMucChucNang Tbl_DanhMucChucNang { get; set; }
        public Guid Fk_DanhMucManHinh { set; get; }
        public virtual Tbl_DanhMucManHinh Tbl_DanhMucManHinh { get; set; }
    }
}
