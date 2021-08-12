using hrmSolution.Models.Models.DanhMucTenQuyenModels;
using hrmSolution.Services.Services.DanhMucTenQuyenService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hrmSolution.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DanhMucTenQuyenController : ControllerBase
    {
        private readonly IDanhMucTenQuyenService _DanhMucTenQuyenService;
        public DanhMucTenQuyenController(IDanhMucTenQuyenService DanhMucTenQuyenService)
        {
            _DanhMucTenQuyenService = DanhMucTenQuyenService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetDanhMucTenQuyenPagingRequest request)
        {
            var lstData = await _DanhMucTenQuyenService.GetAllPaging(request);
            return Ok(lstData);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await _DanhMucTenQuyenService.GetById(id);
            if (data == null)
                return NotFound("Không tìm thấy");
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] DanhMucTenQuyenRequest request)
        {
            var data = await _DanhMucTenQuyenService.Create(request);
            if (!data)
                return BadRequest();
            return Ok("Thêm mới thành công");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] DanhMucTenQuyenRequest request)
        {
            var data = await _DanhMucTenQuyenService.Update(id, request);
            if (!data)
                return BadRequest();
            return Ok("Cập nhật thành công");
        }

        [HttpPut("CapNhatTrangThai/{id}")]
        public async Task<IActionResult> CapNhatTrangThai(Guid id, int trangThai)
        {
            var data = await _DanhMucTenQuyenService.UpdateTrangThai(id, trangThai);
            if (!data)
                return BadRequest();
            return Ok("Cập nhật thành công");
        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await _DanhMucTenQuyenService.Delete(id);
            if (data)
                return Ok("Xóa thành công");
            return BadRequest();
        }
    }
}
