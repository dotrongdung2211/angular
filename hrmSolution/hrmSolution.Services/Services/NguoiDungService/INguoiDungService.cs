using hrmSolution.Models.Models.NguoiDungModels;
using hrmSolution.Models.PublicModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hrmSolution.Services.Services.NguoiDungService
{
    public interface INguoiDungService
    {
        Task<bool> Create(NguoiDungRequest request);
        Task<bool> Update(Guid id, NguoiDungRequest request);
        Task<bool> Delete(Guid id);
        Task<bool> UpdateTrangThai(Guid id, int trangThai);
        Task<PageResult<NguoiDungViewModel>> GetAllPaging(GetNguoiDungPagingRequest request);
        Task<NguoiDungViewModel> GetById(Guid id);
    }
}
