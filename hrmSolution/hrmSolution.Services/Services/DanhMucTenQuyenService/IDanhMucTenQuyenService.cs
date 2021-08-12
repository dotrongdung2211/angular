using hrmSolution.Models.Models.DanhMucTenQuyenModels;
using hrmSolution.Models.PublicModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hrmSolution.Services.Services.DanhMucTenQuyenService
{
    public interface IDanhMucTenQuyenService
    {
        Task<bool> Create(DanhMucTenQuyenRequest request);
        Task<bool> Update(Guid id, DanhMucTenQuyenRequest request);
        Task<bool> Delete(Guid id);
        Task<bool> UpdateTrangThai(Guid id, int trangThai);
        Task<PageResult<DanhMucTenQuyenViewModel>> GetAllPaging(GetDanhMucTenQuyenPagingRequest request);
        Task<DanhMucTenQuyenViewModel> GetById(Guid id);

    }
}
