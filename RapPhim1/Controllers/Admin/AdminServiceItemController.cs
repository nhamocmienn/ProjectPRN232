using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapPhim1.DTO.Admin.serviceItem;
using RapPhim1.Models;
using RapPhim1.Service;

namespace RapPhim1.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/services")]
    public class AdminServiceItemController : ControllerBase
    {
        private readonly IServiceItemService _service;

        public AdminServiceItemController(IServiceItemService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceItemCreateUpdateDTO dto)
        {
            var item = new ServiceItem
            {
                Name = dto.Name,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                IsActive = true
            };

            await _service.AddAsync(item);
            return Ok("Service item created successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ServiceItemCreateUpdateDTO dto)
        {
            var item = await _service.GetEntityByIdAsync(id);
            if (item == null) return NotFound();

            item.Name = dto.Name;
            item.Price = dto.Price;
            item.ImageUrl = dto.ImageUrl;

            await _service.UpdateAsync(item);
            return Ok("Service item updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok("Service item set as inactive");
        }
    }
}
