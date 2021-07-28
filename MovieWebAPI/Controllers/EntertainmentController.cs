using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieWebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EntertainmentController : ControllerBase
    {
        private readonly EntertainmentContext _context;
        public EntertainmentController(EntertainmentContext context)
        {
            _context = context;
        }


        //GET: api/Entertainment
        [HttpGet]
        public async Task<ActionResult<List<Movies>>> GetMovies()
        {
            var movies = await _context.Movies.ToListAsync();
            return movies;
        }

        [HttpGet("{Id}")]
        //GET: api/Movies/1

        public async Task<ActionResult<Movies>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                return movie;
            }
        }

        [HttpDelete("{Id}")]
        //DELETE api/Movies/{Id}
        public async Task<ActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }

        [HttpPost]
        //PUT: api/Movies/{Id}

        public async Task<ActionResult<Movies>> AddMovie(Movies newMovie)

        {
            if (ModelState.IsValid)
            {
                await _context.Movies.AddAsync(newMovie);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetMovie), new { id = newMovie.Id }, newMovie);

            }
            else
            {
                return BadRequest();
            }
        }
        
        [HttpPut("{Id}")]
        //PUT: api/Movies/{Id}
        public async Task<ActionResult> UpdateMovie(int id, Movies updatedMovie)
        {
            if (!ModelState.IsValid || id != updatedMovie.Id)
            {
                return BadRequest();
            }
            else
            {
                var dbMovie = _context.Movies.Find(id);
                dbMovie.Title = updatedMovie.Title;
                dbMovie.Genre = updatedMovie.Genre;
                dbMovie.Runtime = updatedMovie.Runtime;

                _context.Entry(dbMovie).State = EntityState.Modified;
                _context.Update(dbMovie);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }


    }
}
