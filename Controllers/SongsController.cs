using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cbapp.Data;
using cbapp.Models;

namespace cbapp.Controllers
{
    public class SongsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SongsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Songs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Songs.Include(s => s.Project);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songs = await _context.Songs
                .Include(s => s.Project)
                .FirstOrDefaultAsync(m => m.song_id == id);
            if (songs == null)
            {
                return NotFound();
            }

            return View(songs);
        }


        public IActionResult Create(string type)
        {
            var projectId = HttpContext.Session.GetString("p_id");
            if (string.IsNullOrEmpty(projectId))
            {
                return RedirectToAction("Create", "Projects");
            }
            HttpContext.Session.SetString("type", type);
            ViewBag.Type = type;
            ViewBag.trackNum = GetCurrentTracklistNumber(int.Parse(projectId));
            return View();
        }

        private int GetCurrentTracklistNumber(int projectId)
        {
            return _context.Songs.Count(s => s.project_id == projectId);
        }
        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        public async Task<IActionResult> UploadArtwork(IFormFile artwork)
        {

            if (artwork == null || artwork.Length == 0)
            {
                ViewBag.Error = "Selecteaza un fisier valid.";
                return View("Create");
            }

            var min = int.Parse(HttpContext.Session.GetString("min"));
            var projectId = HttpContext.Session.GetString("p_id");
            if (GetCurrentTracklistNumber(int.Parse(projectId)) < min)
            {
                ViewBag.Error = $"Trebuie sa adaugi minim {min} piese pentru acest tip de proiect!";
                ViewBag.Type = HttpContext.Session.GetString("type");
                ViewBag.trackNum = GetCurrentTracklistNumber(int.Parse(projectId));
                return View("Create");
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "projectPhotos");
            var fileExtension = Path.GetExtension(artwork.FileName);
            var fileName = $"{projectId}{fileExtension}";
            var filePath = Path.Combine(uploadsFolder, fileName);


            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await artwork.CopyToAsync(stream);
            }



            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form, [Bind("title")] Songs song)
        {
            var projectId = HttpContext.Session.GetString("p_id");
            var type = HttpContext.Session.GetString("type");
            int currentTracklistNumber = GetCurrentTracklistNumber(int.Parse(projectId));
            Console.WriteLine($"Project ID DIN SONGGGGGGGGGGGS: {projectId}");
            Console.WriteLine($"TYPEEEE DIN SONGSSS: {type}");
            var minutes = form["minutes"];
            var seconds = form["seconds"];
            song.length = $"{minutes}:{seconds}";
            Console.WriteLine($"lengthhhh din songsss: {song.length}");
            int maxSongs = type switch
            {
                "Single" => 1,
                "EP" => 8,
                "Album" => 100,
                _ => 0
            };

            Console.WriteLine($"MAX LEEEEN: {maxSongs}");

            if (currentTracklistNumber >= maxSongs)
            {
                ViewBag.Error = $"Maximul de {maxSongs} piese pentru proiectul {type} a fost atins.";
                ViewBag.Type = type;
                ViewBag.trackNum = GetCurrentTracklistNumber(int.Parse(projectId));
                return View(song);
            }
            song.tracklist_number = currentTracklistNumber + 1;
            song.project_id = int.Parse(projectId);
            Console.WriteLine($"lengthhhh song: {song.length}");
            Console.WriteLine($"titlu song: {song.title}");

            Console.WriteLine($"PROJECT ID song: {song.project_id}");
            Console.WriteLine($"tracklist_number song: {song.tracklist_number}");
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"ModelState Error: {error.ErrorMessage}");
                }
            }
            if (ModelState.IsValid)
            {
                _context.Songs.Add(song);
                await _context.SaveChangesAsync();




                // Continuă cu adăugarea următoarei piese
                return RedirectToAction("Create", new { type });
            }
            ViewBag.Type = type;
            ViewBag.trackNum = GetCurrentTracklistNumber(int.Parse(projectId));
            return View(song);

        }


        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songs = await _context.Songs.FindAsync(id);
            if (songs == null)
            {
                return NotFound();
            }
            ViewData["project_id"] = new SelectList(_context.projects, "project_id", "artist", songs.project_id);
            return View(songs);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("song_id,title,length,project_id,tracklist_number")] Songs songs)
        {
            if (id != songs.song_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(songs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongsExists(songs.song_id))
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
            ViewData["project_id"] = new SelectList(_context.projects, "project_id", "artist", songs.project_id);
            return View(songs);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songs = await _context.Songs
                .Include(s => s.Project)
                .FirstOrDefaultAsync(m => m.song_id == id);
            if (songs == null)
            {
                return NotFound();
            }

            return View(songs);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var songs = await _context.Songs.FindAsync(id);
            if (songs != null)
            {
                _context.Songs.Remove(songs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongsExists(int id)
        {
            return _context.Songs.Any(e => e.song_id == id);
        }
    }
}
