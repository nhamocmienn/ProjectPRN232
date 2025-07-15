using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapPhim1.DTO.Admin;
using RapPhim1.DTO.Admin.showtimes.RapPhim1.DTO.Admin;
using RapPhim1.Models;
using RapPhim1.Service;

namespace RapPhim1.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/showtimes")]
    public class AdminShowtimeController : ControllerBase
    {
        private readonly IShowtimeService _service;
        private readonly IMovieService _movieService;

        public AdminShowtimeController(IShowtimeService service, IMovieService movieService)
        {
            _service = service;
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ShowtimeCreateUpdateDto dto)
        {
            var movie = await _movieService.GetByIdAsync(dto.MovieId);
            if (movie == null) return NotFound("Movie not found");

            var conflict = await _service.IsConflictAsync(dto.RoomId, dto.StartTime, movie.Duration);
            if (conflict) return BadRequest("Schedule conflict in the same room");

            var showtime = new Showtime
            {
                MovieId = dto.MovieId,
                RoomId = dto.RoomId,
                StartTime = dto.StartTime,
                IsActive = true
            };

            await _service.AddAsync(showtime);
            return Ok("Showtime created successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ShowtimeCreateUpdateDto dto)
        {
            var showtime = await _service.GetEntityByIdAsync(id); // 👈 Lấy entity gốc
            if (showtime == null) return NotFound();

            var movie = await _movieService.GetByIdAsync(dto.MovieId);
            if (movie == null) return NotFound("Movie not found");

            var conflict = await _service.IsConflictAsync(dto.RoomId, dto.StartTime, movie.Duration, id);
            if (conflict) return BadRequest("Schedule conflict in the same room");

            showtime.MovieId = dto.MovieId;
            showtime.RoomId = dto.RoomId;
            showtime.StartTime = dto.StartTime;

            await _service.UpdateAsync(showtime);
            return Ok("Showtime updated successfully");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok("Showtime deleted (set inactive)");
        }
    }
}
