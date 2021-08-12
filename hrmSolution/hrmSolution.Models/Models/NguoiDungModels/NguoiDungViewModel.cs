using System;
using System.Collections.Generic;
using System.Text;

namespace hrmSolution.Models.Models.NguoiDungModels
{
    public class NguoiDungViewModel
    {
        public Guid Id { set; get; }
        public string C_Code { set; get; }
        public string C_Email { set; get; }
        public string C_HoVaTen { set; get; }
        public string C_SoDienThoai { set; get; }
        public DateTime C_NgayTao { set; get; }
        public string C_Avatar { get; set; }
        public int C_TrangThai { get; set; }

        public string C_TenQuyen { get; set; }
        public Guid Fk_DanhMucTenQuyen { set; get; }
    }
}
