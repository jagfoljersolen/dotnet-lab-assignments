using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab6.Data;
using Lab6.Models;

namespace Lab6.Controllers
{
    public class HomeController : Controller
    {
        private readonly MoviesDbContext _context;

        public HomeController(MoviesDbContext context)
        {
            _context = context;
        }

        // GET: Home
        public async Task<IActionResult> Index()
        {
            return _context.Movies != null
                ? View(await _context.Movies.Include(x => x.Genre).ToListAsync())
                : Problem("Entity set 'MoviesDbContext.Movies'  is null.");
        }

        // GET: Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Home/Create
        public IActionResult Create()
        {
            var m = new MovieDto
            {
                AllGenres = _context.Genre.Select(x => x.Name).ToList()
            };
            return View(m);
        }

        // POST: Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Title,Description,Rating,TrailerLink, Genre")] MovieDto movie
        )
        {
            if (ModelState.IsValid)
            {
                var genre = _context.Genre.FirstOrDefault(x => x.Name == movie.Genre);
                if (genre == null)
                {
                    genre = new Genre { Id = 0, Name = movie.Genre ?? string.Empty };
                }
                Movie m = new Movie
                {
                    Id = 0,
                    Title = movie.Title,
                    Description = movie.Description,
                    Rating = movie.Rating,
                    TrailerLink = movie.TrailerLink,
                    Genre = genre
                };
                _context.Add(m);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(x => x.Genre)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            var movieDto = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Rating = movie.Rating,
                TrailerLink = movie.TrailerLink,
                Genre = movie.Genre?.Name,
                AllGenres = _context.Genre.Select(x => x.Name).ToList()
            };
            return View(movieDto);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Title,Description,Rating,TrailerLink,Genre")] MovieDto movie
        )
        {
            
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)

        {
            try
            {
                var entity = await _context.Movies
                    .Include(x => x.Genre)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (entity == null)
                {
                    return NotFound();
                }

                entity.Title = movie.Title;
                entity.Description = movie.Description;
                entity.Rating = movie.Rating;
                entity.TrailerLink = movie.TrailerLink;

                if (!string.IsNullOrWhiteSpace(movie.Genre))
                {
                    var genre = await _context.Genre
                        .FirstOrDefaultAsync(x => x.Name == movie.Genre);

                    if (genre == null)
                    {
                        genre = new Genre { Name = movie.Genre };
                        _context.Genre.Add(genre);
                    }

                    entity.Genre = genre;
                }
                else
                {
                    entity.Genre = null;
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
            return View(movie);
        }

        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movies == null)
            {
                return Problem("Entity set 'MoviesDbContext.Movies'  is null.");
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
