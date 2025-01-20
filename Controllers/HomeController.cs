using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using cbapp.Models;
using Microsoft.EntityFrameworkCore;
using cbapp.Data;

namespace cbapp.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> IndexAsync()
    {
        var projects = await _context.projects.ToListAsync();
        return View(projects);
    }

    [HttpGet()]
    public async Task<IActionResult> Search(string searchTerm)
    {
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
}
