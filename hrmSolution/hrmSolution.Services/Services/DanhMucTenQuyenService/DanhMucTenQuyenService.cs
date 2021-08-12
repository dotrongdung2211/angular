using hrmSolution.Data.EF;
using hrmSolution.Data.Entities;
using hrmSolution.Models.Models.DanhMucTenQuyenModels;
using hrmSolution.Models.PublicModels;
using hrmSolution.Services.Enums;
using hrmSolution.Util.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hrmSolution.Services.Services.DanhMucTenQuyenService
{
    public class DanhMucTenQuyenService : IDanhMucTenQuyenService
    {
        private readonly HrmDbContext _context;
        public DanhMucTenQuyenService(HrmDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create( DanhMucTenQuyenRequest request)
        {
            var isExist = await _context.Tbl_DanhMucTenQuyen.Where(x => x.C_Code == request.Code).AnyAsync();
            if (!isExist)
            {
                var danhMucTenQuyen = new Tbl_DanhMucTenQuyen()
                {
                    Id = Guid.NewGuid(),
                    C_Code = request.Code,
                    C_TenQuyen = request.TenQuyen,
                    C_NgayTao = DateTime.Now,
                    C_TrangThai = request.TrangThai,
                    C_IsDeleted = 2
                };
                _context.Tbl_DanhMucTenQuyen.Add(danhMucTenQuyen);
                return await _context.SaveChangesAsync() > 0;
            }
            else
            {
                throw new HrmException("Trùng mã bản ghi");
            }

        }

        public async Task<bool> Update(Guid id,  DanhMucTenQuyenRequest request)
        {
            var isExist = await _context.Tbl_DanhMucTenQuyen.Where(x => x.Id == id).AnyAsync();
            if (!isExist) throw new HrmException("Không tìm thấy bản ghi");
            var danhMucTenQuyen = await _context.Tbl_DanhMucTenQuyen.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(danhMucTenQuyen.C_Code != request.Code)
            {
                var isExistCode = await _context.Tbl_DanhMucTenQuyen.Where(x => x.C_Code == request.Code).AnyAsync();
                if (isExistCode) throw new HrmException("Trùng mã bản ghi");
                danhMucTenQuyen.C_Code = request.Code;
            }
            danhMucTenQuyen.C_TenQuyen = request.TenQuyen;
            danhMucTenQuyen.C_TrangThai = request.TrangThai;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PageResult<DanhMucTenQuyenViewModel>> GetAllPaging(GetDanhMucTenQuyenPagingRequest request)
        {
            var query = await _context.Tbl_DanhMucTenQuyen.Where(x => x.C_IsDeleted != (int)TrangThaiXoa.IS_DELETED).ToListAsync();
            if (!string.IsNullOrEmpty(request.KeyWord))
                query = query.Where(x => x.C_TenQuyen.Contains(request.KeyWord)).ToList();
            int totalRow = query.Count();
            var data = query.Skip(request.PageIndex - 1 * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new DanhMucTenQuyenViewModel()
                {
                    Id = x.Id,
                    C_Code = x.C_Code,
                    C_TenQuyen = x.C_TenQuyen,
                    C_TrangThai = x.C_TrangThai
                })
                .ToList();
            var pageResult = new PageResult<DanhMucTenQuyenViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pageResult;
        }

        public async Task<bool> UpdateTrangThai(Guid id, int trangThai)
        {
            var danhMucTenQuyen = await _context.Tbl_DanhMucTenQuyen
                .Where(x => x.C_IsDeleted != (int)TrangThaiXoa.IS_DELETED && x.Id == id)
                .FirstOrDefaultAsync();
            if (danhMucTenQuyen == null) throw new HrmException("Không tìm thấy bản ghi");
            danhMucTenQuyen.C_TrangThai = trangThai;
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Delete(Guid id)
        {
            var danhMucTenQuyen = await _context.Tbl_DanhMucTenQuyen
                .Where(x => x.C_IsDeleted != (int)TrangThaiXoa.IS_DELETED && x.Id == id)
                .FirstOrDefaultAsync();
            if (danhMucTenQuyen == null) throw new HrmException("Không tìm thấy bản ghi");
            danhMucTenQuyen.C_IsDeleted = 1;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<DanhMucTenQuyenViewModel> GetById(Guid id)
        {
            var danhMucTenQuyen = await _context.Tbl_DanhMucTenQuyen
                .Where(x => x.C_IsDeleted != (int)TrangThaiXoa.IS_DELETED && x.Id == id)
                .Select(x => new DanhMucTenQuyenViewModel()
                {
                    Id = x.Id,
                    C_Code = x.C_Code,
                    C_TenQuyen = x.C_TenQuyen,
                    C_TrangThai = x.C_TrangThai
                })
                .FirstOrDefaultAsync();
            if(danhMucTenQuyen == null) throw new HrmException("Không tìm thấy bản ghi");
            return danhMucTenQuyen;
        }
    }
}
