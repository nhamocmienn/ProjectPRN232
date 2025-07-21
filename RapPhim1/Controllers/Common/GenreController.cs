using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapPhim1.Models;
using RapPhim1.Service;

namespace RapPhim1.Controllers.Common
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Genre>>> GetAll()
        {
            var genres = await _genreService.GetAllAsync();
            return Ok(genres);
        }
    }
}
