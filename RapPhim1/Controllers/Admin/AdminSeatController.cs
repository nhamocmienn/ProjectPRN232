using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapPhim1.DTO.Admin.seat;
using RapPhim1.Models;
using RapPhim1.Service;

namespace RapPhim1.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/seats")]
    public class AdminSeatController : ControllerBase
    {
        private readonly ISeatService _seatService;
        public AdminSeatController(ISeatService seatService) => _seatService = seatService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var seats = await _seatService.GetAllAsync(); 
            return Ok(seats);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var seat = await _seatService.GetByIdAsync(id);
            return seat == null ? NotFound() : Ok(seat);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SeatCreateDTO dto)
        {
            var seat = new Seat
            {
                RoomId = dto.RoomId,
                Row = dto.Row,
                Column = dto.Column,
                SeatTypeId = dto.SeatTypeId
            };
            await _seatService.AddAsync(seat);
            return Ok("Seat created successfully");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SeatUpdateDTO dto)
        {
            if (id != dto.Id) return BadRequest("Mismatched seat ID");

            var existing = await _seatService.GetByIdAsync(id);
            if (existing == null) return NotFound("Seat not found");

            // Cập nhật thủ công từ DTO
            existing.RoomId = dto.RoomId;
            existing.Row = dto.Row;
            existing.Column = dto.Column;
            existing.SeatTypeId = dto.SeatTypeId;
            existing.IsActive = dto.IsActive;

            await _seatService.UpdateAsync(existing);
            return Ok("Seat updated successfully");
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _seatService.DeleteAsync(id);
            return Ok("Seat deactivated successfully");
        }
    }
}
