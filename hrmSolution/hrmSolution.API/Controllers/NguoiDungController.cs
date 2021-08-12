using hrmSolution.Models.Models.NguoiDungModels;
using hrmSolution.Services.Services.NguoiDungService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hrmSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungController : ControllerBase
    {
        private readonly INguoiDungService _NguoiDungService;

        public NguoiDungController(INguoiDungService NguoiDungService)
        {
            _NguoiDungService = NguoiDungService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetNguoiDungPagingRequest request)
        {
            var lstData = await _NguoiDungService.GetAllPaging(request);
            return Ok(lstData);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await _NguoiDungService.GetById(id);
            if (data == null)
                return NotFound("Không tìm thấy");
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] NguoiDungRequest request)
        {
            var data = await _NguoiDungService.Create(request);
            if (!data)
                return BadRequest();
            return Ok("Thêm mới thành công");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] NguoiDungRequest request)
        {
            var data = await _NguoiDungService.Update(id, request);
            if (!data)
                return BadRequest();
            return Ok("Cập nhật thành công");
        }

        [HttpPut("CapNhatTrangThai/{id}")]
        public async Task<IActionResult> CapNhatTrangThai(Guid id, int trangThai)
        {
            var data = await _NguoiDungService.UpdateTrangThai(id, trangThai);
            if (!data)
                return BadRequest();
            return Ok("Cập nhật thành công");
        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await _NguoiDungService.Delete(id);
            if (data)
                return Ok("Xóa thành công");
            return BadRequest();
        }
    }
}
