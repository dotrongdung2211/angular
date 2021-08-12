using System;
using System.Collections.Generic;
using System.Text;

namespace hrmSolution.Data.Entities
{
    public class Tbl_NguoiDung
    {
        public Guid Id { set; get; }
        public string C_Code { set; get; }
        public string C_Email { set; get; }
        public string C_MatKhau { set; get; }
        public string C_HoVaTen { set; get; }
        public string C_SoDienThoai { set; get; }
        public DateTime C_NgayTao { set; get; }
        public string C_Avatar { get; set; }
        public int C_TrangThai { get; set; }
        public int C_IsDeleted { get; set; }

        public Guid Fk_DanhMucTenQuyen { set; get; }
        public virtual Tbl_DanhMucTenQuyen Tbl_DanhMucTenQuyen { get; set; }
    }
}
