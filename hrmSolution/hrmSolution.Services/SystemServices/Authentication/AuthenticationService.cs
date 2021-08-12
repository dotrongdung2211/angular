using hrmSolution.Data.EF;
using hrmSolution.Models.System;
using hrmSolution.Services.Enums;
using hrmSolution.Services.Services.NguoiDungService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace hrmSolution.Services.SystemServices.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HrmDbContext _context;
        private readonly INguoiDungService _NguoiDungService;
        public readonly IConfiguration _configuration;

        public AuthenticationService(HrmDbContext context, INguoiDungService NguoiDungService, IConfiguration configuration)
        {
            _context = context;
            _NguoiDungService = NguoiDungService;
            _configuration = configuration;
        }

        public async Task<string> Login(LoginRequest request)
        {
            var passwordHash = Md5Hash(request.CMatKhau);
            var user = await _context.Tbl_NguoiDung.Where(x =>
                x.C_Email == request.CTaiKhoan
                && x.C_MatKhau == passwordHash
                && x.C_TrangThai == (int)TrangThai.ACTIVE
                && x.C_IsDeleted == (int)TrangThaiXoa.STILL_ALIVE
                ).FirstOrDefaultAsync();
            if (user == null) return null;
            var danhMucTenQuyen = await _context.Tbl_DanhMucTenQuyen.Where(x => x.Id == user.Fk_DanhMucTenQuyen).FirstOrDefaultAsync();
            if (danhMucTenQuyen == null) return null;
            var claims = new[]
            {
                   new Claim(ClaimTypes.Email, user.C_Email),
                   new Claim(ClaimTypes.GivenName, user.C_HoVaTen),
                   new Claim(ClaimTypes.HomePhone, user.C_SoDienThoai),
                   new Claim("RoleName", danhMucTenQuyen.C_TenQuyen),
                   new Claim("RoleId", user.Fk_DanhMucTenQuyen.ToString()),
                   new Claim("Avatar", user.C_Avatar),
                   new Claim("Code", user.C_Code),
                   new Claim("TrangThai", user.C_TrangThai.ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Tokens:Issuer"],
                _configuration["Tokens:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

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
