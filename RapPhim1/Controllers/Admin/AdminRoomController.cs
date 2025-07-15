using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapPhim1.Models;
using RapPhim1.Service;

namespace RapPhim1.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/rooms")]
    public class AdminRoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public AdminRoomController(IRoomService roomService) => _roomService = roomService;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _roomService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var room = await _roomService.GetByIdAsync(id);
            return room == null ? NotFound() : Ok(room);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Room room)
        {
            await _roomService.AddAsync(room);
            return Ok("Room created successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Room room)
        {
            if (id != room.Id) return BadRequest();
            await _roomService.UpdateAsync(room);
            return Ok("Room updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _roomService.DeleteAsync(id);
            return Ok("Room deactivated successfully");
        }
    }

    

}
