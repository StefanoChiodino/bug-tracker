using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BugTracker.Web.Models;

namespace BugTracker.Web.Controllers
{
    [Route("api/bug/[action]")]
    [ApiController]
    public class BugApiController : Controller
    {
        private readonly BugTrackerDbContext _context;

        public BugApiController(BugTrackerDbContext context)
        {
            _context = context;
        }

        // GET: Bug
        public async Task<IActionResult> Index()
        {
            // TODO: probably bad karma, don't share this view or handle without using ViewData.
            ViewData["Title"] = "Open Bugs";
            
            var openBugs = await _context.Bugs
                // I recently started to prefer using `== false` to ! because it's more obvious
                .Where(bug => bug.IsClosed == false)
                .ToListAsync();
            
            return Json(openBugs);
        }

        public async Task<IActionResult> Closed()
        {
            // TODO: probably bad karma, don't share this view or handle without using ViewData.
            ViewData["Title"] = "Closed Bugs";

            var closedBugs = await _context.Bugs
                // I recently started to prefer using `== false` to ! because it's more obvious
                .Where(bug => bug.IsClosed)
                .ToListAsync();
            
            return Json(closedBugs);
        }

        // GET: Bug/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bugs
                .FirstOrDefaultAsync(m => m.BugId == id);
            if (bug == null)
            {
                return NotFound();
            }

            return Json(bug);
        }
    }
}
