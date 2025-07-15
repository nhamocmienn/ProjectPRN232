using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapPhim1.DTO.Admin.movie;
using RapPhim1.Models;
using RapPhim1.Service;

namespace RapPhim1.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/movies")]
    public class AdminMovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public AdminMovieController(IMovieService movieService) => _movieService = movieService;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _movieService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);
            if (movie == null) return NotFound();

            var dto = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                Duration = movie.Duration,
                Description = movie.Description,
                PosterUrl = movie.PosterUrl,
                LandscapeImageUrl = movie.LandscapeImageUrl,
                TrailerUrl = movie.TrailerUrl,
                IsActive = movie.IsActive,
                ActorNames = movie.MovieActors.Select(ma => ma.Actor.Name).ToList(),
                DirectorNames = movie.MovieDirectors.Select(md => md.Director.Name).ToList(),
                GenreNames = movie.MovieGenres.Select(mg => mg.Genre.Name).ToList()
            };

            return Ok(dto);
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MovieCreateDto dto)
        {
            var movie = new Movie
            {
                Title = dto.Title,
                ReleaseDate = dto.ReleaseDate,
                Duration = dto.Duration,
                Description = dto.Description,
                PosterUrl = dto.PosterUrl,
                LandscapeImageUrl = dto.LandscapeImageUrl,
                TrailerUrl = dto.TrailerUrl
            };

            await _movieService.AddMovieAsync(movie, dto.ActorNames, dto.DirectorNames, dto.GenreIds);
            return Ok("Movie created successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MovieUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var movie = await _movieService.GetByIdAsync(id);
            if (movie == null) return NotFound();

            // cập nhật các trường
            movie.Title = dto.Title;
            movie.ReleaseDate = dto.ReleaseDate;
            movie.Duration = dto.Duration;
            movie.Description = dto.Description;
            movie.PosterUrl = dto.PosterUrl;
            movie.LandscapeImageUrl = dto.LandscapeImageUrl;
            movie.TrailerUrl = dto.TrailerUrl;
            movie.IsActive = dto.IsActive;

            await _movieService.UpdateAsync(movie, dto.ActorNames, dto.DirectorNames, dto.GenreIds);
            return Ok("Movie updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _movieService.DeleteAsync(id);
            return Ok("Movie deactivated successfully");
        }

    }

}
