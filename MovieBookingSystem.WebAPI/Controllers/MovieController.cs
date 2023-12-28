using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieBookingSystem.Core.Models;
using MovieBookingSystem.Domain.IRepositories;

namespace MovieMovieSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        
        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public IEnumerable<Movie> GetAll()
        {
            return _movieRepository.GetMovies();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var movie = _movieRepository.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }

            return new ObjectResult(movie);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Movie movie)
        {
            if (movie == null)
            {
                return BadRequest();
            }

            return new ObjectResult(_movieRepository.CreateMovie(movie));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Movie movie)
        {
            if (movie == null || movie.Id != id)
            {
                return BadRequest();
            }
            return new ObjectResult(_movieRepository.UpdateMovie(movie));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_movieRepository.GetMovieById(id) == null)
            {
                return NotFound();
            }
            _movieRepository.DeleteMovie(id);
            return NoContent();
        }
    }
}
