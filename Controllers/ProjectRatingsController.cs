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
    public class ProjectRatingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectRatingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProjectRatings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProjectRatings.Include(p => p.Project).Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProjectRatings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectRatings = await _context.ProjectRatings
                .Include(p => p.Project)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.projectId == id);
            if (projectRatings == null)
            {
                return NotFound();
            }

            return View(projectRatings);
        }

        // GET: ProjectRatings/Create
        public IActionResult Create()
        {
            ViewData["projectId"] = new SelectList(_context.projects, "project_id", "artist");
            ViewData["UserId"] = new SelectList(_context.CustomUsers, "Id", "Id");
            return View();
        }

        // POST: ProjectRatings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("projectId,UserId,rating_date,score")] ProjectRatings projectRatings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectRatings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["projectId"] = new SelectList(_context.projects, "project_id", "artist", projectRatings.projectId);
            ViewData["UserId"] = new SelectList(_context.CustomUsers, "Id", "Id", projectRatings.UserId);
            return View(projectRatings);
        }

        // GET: ProjectRatings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectRatings = await _context.ProjectRatings.FindAsync(id);
            if (projectRatings == null)
            {
                return NotFound();
            }
            ViewData["projectId"] = new SelectList(_context.projects, "project_id", "artist", projectRatings.projectId);
            ViewData["UserId"] = new SelectList(_context.CustomUsers, "Id", "Id", projectRatings.UserId);
            return View(projectRatings);
        }

        // POST: ProjectRatings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("projectId,UserId,rating_date,score")] ProjectRatings projectRatings)
        {
            if (id != projectRatings.projectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectRatings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectRatingsExists(projectRatings.projectId))
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
            ViewData["projectId"] = new SelectList(_context.projects, "project_id", "artist", projectRatings.projectId);
            ViewData["UserId"] = new SelectList(_context.CustomUsers, "Id", "Id", projectRatings.UserId);
            return View(projectRatings);
        }

        // GET: ProjectRatings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectRatings = await _context.ProjectRatings
                .Include(p => p.Project)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.projectId == id);
            if (projectRatings == null)
            {
                return NotFound();
            }

            return View(projectRatings);
        }

        // POST: ProjectRatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectRatings = await _context.ProjectRatings.FindAsync(id);
            if (projectRatings != null)
            {
                _context.ProjectRatings.Remove(projectRatings);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectRatingsExists(int id)
        {
            return _context.ProjectRatings.Any(e => e.projectId == id);
        }
    }
}
