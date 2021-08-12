using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace hrmSolution.Models.Models.DanhMucTenQuyenModels
{
    public class DanhMucTenQuyenRequest
    {
        public string Code { get; set; }
        public string TenQuyen { get; set; }
        public int TrangThai { get; set; }
    }
}
