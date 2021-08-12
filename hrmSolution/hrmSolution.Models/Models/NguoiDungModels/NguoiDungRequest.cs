using System;
using System.Collections.Generic;
using System.Text;

namespace hrmSolution.Models.Models.NguoiDungModels
{
    public class NguoiDungRequest
    {
        public string C_Code { set; get; }
        public string C_Email { set; get; }
        public string C_MatKhau { set; get; }
        public string C_HoVaTen { set; get; }
        public string C_SoDienThoai { set; get; }
        public string C_Avatar { get; set; }
        public int C_TrangThai { get; set; }
        public Guid Fk_DanhMucTenQuyen { set; get; }
    }
}
