using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapPhim1.DTO.Admin.actor;
using RapPhim1.Service;

namespace RapPhim1.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/actors")]
    public class AdminActorController : ControllerBase
    {
        private readonly IActorService _service;
        public AdminActorController(IActorService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var actor = await _service.GetByIdAsync(id);
            return actor == null ? NotFound() : Ok(actor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ActorCreateUpdateDto dto)
        {
            await _service.AddAsync(dto);
            return Ok("Actor created");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ActorCreateUpdateDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return Ok("Actor updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok("Actor deleted (set inactive)");
        }
    }

}
