using hrmSolution.Data.EF;
using hrmSolution.Data.Entities;
using hrmSolution.Models.Models.NguoiDungModels;
using hrmSolution.Models.PublicModels;
using hrmSolution.Services.Enums;
using hrmSolution.Util.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace hrmSolution.Services.Services.NguoiDungService
{
    public class NguoiDungService : INguoiDungService
    {
        private readonly HrmDbContext _context;
        public NguoiDungService(HrmDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(NguoiDungRequest request)
        {
            var checkDanhMucTenQuyen = await _context.Tbl_DanhMucTenQuyen.Where(x => x.Id == request.Fk_DanhMucTenQuyen).AnyAsync();
            if (!checkDanhMucTenQuyen) throw new HrmException("Không tồn tại mã tên quyền");
            var isExist = await _context.Tbl_NguoiDung.Where(x => x.C_Code == request.C_Code).AnyAsync();
            if (!isExist)
            {
                var nguoiDung = new Tbl_NguoiDung()
                {
                    Id = Guid.NewGuid(),
                    C_Code = request.C_Code,
                    C_Email = request.C_Email,
                    C_HoVaTen = request.C_HoVaTen,
                    C_MatKhau = Md5Hash(request.C_MatKhau),
                    C_SoDienThoai = request.C_SoDienThoai,
                    C_Avatar = "default",
                    Fk_DanhMucTenQuyen = request.Fk_DanhMucTenQuyen,
                    C_NgayTao = DateTime.Now,
                    C_TrangThai = request.C_TrangThai,
                    C_IsDeleted = 2
                };
                _context.Tbl_NguoiDung.Add(nguoiDung);
                return await _context.SaveChangesAsync() > 0;
            }
            else
            {
                throw new HrmException("Trùng mã bản ghi");
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            var nguoiDung = await _context.Tbl_NguoiDung
                .Where(x => x.C_IsDeleted != (int)TrangThaiXoa.IS_DELETED && x.Id == id)
                .FirstOrDefaultAsync();
            if (nguoiDung == null) throw new HrmException("Không tìm thấy bản ghi");
            nguoiDung.C_IsDeleted = 1;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PageResult<NguoiDungViewModel>> GetAllPaging(GetNguoiDungPagingRequest request)
        {
            var query = await _context.Tbl_NguoiDung.Where(x => x.C_IsDeleted != (int)TrangThaiXoa.IS_DELETED).ToListAsync();
            if (!string.IsNullOrEmpty(request.KeyWord))
                query = query.Where(x => x.C_HoVaTen.Contains(request.KeyWord)).ToList();
            int totalRow = query.Count();
            var lstDanhMucTenQuyen = await _context.Tbl_DanhMucTenQuyen.ToListAsync();
            if (lstDanhMucTenQuyen == null) throw new HrmException("Danh mục tên quyền không tồn tại");
            var data = query.Skip(request.PageIndex - 1 * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new NguoiDungViewModel()
                {
                    Id = x.Id,
                    C_Code = x.C_Code,
                    C_HoVaTen = x.C_HoVaTen,
                    C_Email = x.C_Email,
                    C_SoDienThoai = x.C_SoDienThoai,
                    C_NgayTao = x.C_NgayTao,
                    C_Avatar = x.C_Avatar,
                    C_TrangThai = x.C_TrangThai,
                    C_TenQuyen = lstDanhMucTenQuyen.Where(c => c.Id == x.Fk_DanhMucTenQuyen).Select(c => c.C_TenQuyen).FirstOrDefault().ToString(),
                    Fk_DanhMucTenQuyen = x.Fk_DanhMucTenQuyen
                })
                .ToList();
            var pageResult = new PageResult<NguoiDungViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pageResult;
        }

        public async Task<NguoiDungViewModel> GetById(Guid id)
        {
            var nguoiDung = await _context.Tbl_NguoiDung
               .Where(x => x.C_IsDeleted != (int)TrangThaiXoa.IS_DELETED && x.Id == id)
               .FirstOrDefaultAsync();
            if (nguoiDung == null) throw new HrmException("Không tìm thấy bản ghi");
            var danhMucTenQuyen = await _context.Tbl_DanhMucTenQuyen.Where(x => x.Id == nguoiDung.Fk_DanhMucTenQuyen).FirstOrDefaultAsync();
            if (danhMucTenQuyen == null) throw new HrmException("Danh mục tên quyền không tồn tại");
            return new NguoiDungViewModel()
            {
                Id = nguoiDung.Id,
                C_Code = nguoiDung.C_Code,
                C_HoVaTen = nguoiDung.C_HoVaTen,
                C_Email = nguoiDung.C_Email,
                C_SoDienThoai = nguoiDung.C_SoDienThoai,
                C_NgayTao = nguoiDung.C_NgayTao,
                C_Avatar = nguoiDung.C_Avatar,
                C_TrangThai = nguoiDung.C_TrangThai,
                C_TenQuyen = danhMucTenQuyen.C_TenQuyen,
                Fk_DanhMucTenQuyen = nguoiDung.Fk_DanhMucTenQuyen
            };
        }

        public async Task<bool> Update(Guid id, NguoiDungRequest request)
        {
            var isExist = await _context.Tbl_NguoiDung.Where(x => x.Id == id).AnyAsync();
            if (!isExist) throw new HrmException("Không tìm thấy bản ghi");
            var nguoiDung = await _context.Tbl_NguoiDung.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (nguoiDung.C_Code != request.C_Code)
            {
                var isExistCode = await _context.Tbl_NguoiDung.Where(x => x.C_Code == request.C_Code).AnyAsync();
                if (isExistCode) throw new HrmException("Trùng mã bản ghi");
                nguoiDung.C_Code = request.C_Code;
            }
            nguoiDung.C_Email = request.C_Email;
            nguoiDung.C_HoVaTen = request.C_HoVaTen;
            nguoiDung.C_MatKhau = Md5Hash(request.C_MatKhau);
            nguoiDung.C_SoDienThoai = request.C_SoDienThoai;
            nguoiDung.C_Avatar = "default";
            nguoiDung.Fk_DanhMucTenQuyen = request.Fk_DanhMucTenQuyen;
            nguoiDung.C_TrangThai = request.C_TrangThai;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateTrangThai(Guid id, int trangThai)
        {
            var nguoiDung = await _context.Tbl_NguoiDung
               .Where(x => x.C_IsDeleted != (int)TrangThaiXoa.IS_DELETED && x.Id == id)
               .FirstOrDefaultAsync();
            if (nguoiDung == null) throw new HrmException("Không tìm thấy bản ghi");
            nguoiDung.C_TrangThai = trangThai;
            return await _context.SaveChangesAsync() > 0;
        }

        private string Md5Hash(string password)
        {
            //Tạo MD5 
            MD5 mh = MD5.Create();
            //Chuyển kiểu chuổi thành kiểu byte
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
            //mã hóa chuỗi đã chuyển
            byte[] hash = mh.ComputeHash(inputBytes);
            //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}