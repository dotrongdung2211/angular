using System;
using System.Collections.Generic;
using System.Text;

namespace hrmSolution.Models.Models.DanhMucTenQuyenModels
{
    public class DanhMucTenQuyenViewModel
    {
        public Guid Id { get; set; }
        public string C_Code { get; set; }
        public string C_TenQuyen { get; set; }
        public int C_TrangThai { get; set; }

    }
}
