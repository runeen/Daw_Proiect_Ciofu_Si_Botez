using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using cbapp.Models;
using Microsoft.EntityFrameworkCore;
using cbapp.Data;
using Microsoft.AspNetCore.Identity;

namespace cbapp.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<IdentityUser> _userManager;

    public HomeController(ApplicationDbContext context, UserManager<IdentityUser> um)
    {
        _context = context;
        _userManager = um;
    }

    public async Task<IActionResult> IndexAsync()
    {
        var projects = await _context.projects.ToListAsync();
        return View(projects);
    }

    [HttpGet()]
    public async Task<IActionResult> Search(string searchTerm)
    {
        //comentariu
        if (string.IsNullOrEmpty(searchTerm))
        {
            return View(new List<Project>());
        }

        var projects = await _context.projects
            .Where(p => EF.Functions.Like(p.release_title, $"%{searchTerm}%"))
            .ToListAsync();

        ViewData["Debug"] = searchTerm;
        return View(projects);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<IActionResult> rating_dashboard()
    {
        var applicationDbContext = _context.ProjectRatings;
        var rez = await applicationDbContext.Where(c => c.UserId == _userManager.GetUserId(this.User)).Include(c => c.Project).OrderBy(c => c.rating_date).ToListAsync();
        ViewData["rez"] = rez.Count;
        return View(rez);
    }
}
