using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cbapp.Data;
using cbapp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Build.Evaluation;

namespace cbapp.Controllers
{
    public class ProjectRatingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;

        public ProjectRatingsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ProjectRatings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProjectRatings;
            var rez = await applicationDbContext.ToListAsync();
            ViewData["rez"] = rez.Count;
            return View(rez);
        }

        // GET: ProjectRatings/Details/5

        [HttpGet()]
        public async Task<IActionResult> Details(int? projectId, string? userId)
        {
            //if (projectId == null || userId == null)
            //{
            //    return NotFound();
            //}

            /*
             * 
                var projects = await _context.projects
                    .Where(p => EF.Functions.Like(p.release_title, $"%{searchTerm}%"))
                    .ToListAsync();
             */

            var projectRatings = await _context.ProjectRatings
                .Where(c => c.projectId == projectId)
                .Where(c => c.UserId == userId)
                .FirstAsync();
            if (projectRatings == null)
            {
                return NotFound();
            }

            return View(projectRatings);
        }

        [HttpGet()]
        public async Task<IActionResult> CreateFromFront(int? project_id)
        {
            String userId = "";
            if (this.User != null)
            {
                userId = _userManager.GetUserId(this.User);
            }

            

            ViewData["Title"] = "";
            ViewData["UserId"] = new List<SelectListItem> {
                new SelectListItem { Text = userId, Value = userId}
            };
            //ViewData["projectId"] = new SelectList(_context.projects, "project_id", "release_title");
            ViewData["projectId"] = new List<SelectListItem> {
                new SelectListItem { Text = project_id.ToString(), Value = project_id.ToString()}
            };
            return View();
        }

        // GET: ProjectRatings/Create
        public IActionResult Create()
        {
            String userId = "";
            if (this.User != null)
            {
                userId = _userManager.GetUserId(this.User);
            }
            ViewData["Title"] = "";
            ViewData["UserId"] = new List<SelectListItem> {
                new SelectListItem { Text = userId, Value = userId}
            };
            ViewData["projectId"] = new SelectList(_context.projects, "project_id", "release_title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId, projectId, score, create")] ProjectRatings projectRating)
        {
            try
            {
                var projectRatings = await _context.ProjectRatings
                        .Where(c => c.projectId == projectRating.projectId)
                        .Where(c => c.UserId == projectRating.UserId)
                        .FirstAsync();
                if (projectRatings != null)
                {
                    _context.ProjectRatings.Remove(projectRatings);
                }
            }
            catch { }


            

            //if (!TryValidateModel(songs))
            //{
            //    ViewData["Debug"] =  $"{songs.song_id}, {songs.title}, {songs.length}, {songs.project_id}, {songs.tracklist_number}";
            //}
            projectRating.rating_date = DateTime.Now;

            


            try
            {
                _context.Add(projectRating);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
            }

            // -- asta te trimite inapoi in create daca e ceva gresit da dau bypass
            //ViewData["project_id"] = new SelectList(_context.projects, "project_id", "artist", songs.project_id);
            //return View(songs);
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
        public async Task<IActionResult> Delete(int? projectId, string? UserId)
        {
            if (projectId == null || UserId == null)
            {
                return NotFound();
            }

            var projectRatings = await _context.ProjectRatings
                .Where(c => c.projectId == projectId)
                .Where(c => c.UserId == UserId)
                .FirstAsync();
            if (projectRatings == null)
            {
                return NotFound();
            }

            return View(projectRatings);
        }


        // POST: ProjectRatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? projectId, string? UserId)
        {
            var projectRatings = await _context.ProjectRatings
                .Where(c => c.projectId == projectId)
                .Where(c => c.UserId == UserId)
                .FirstAsync();

            if (projectRatings != null)
            {
                _context.ProjectRatings.Remove(projectRatings);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("rating_dashboard", "Home");
        }

        private bool ProjectRatingsExists(int id)
        {
            return _context.ProjectRatings.Any(e => e.projectId == id);
        }
    }
}
