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
    public class BugController : Controller
    {
        private readonly BugTrackerDbContext _context;

        public BugController(BugTrackerDbContext context)
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
            
            return View(openBugs);
        }

        public async Task<IActionResult> Closed()
        {
            // TODO: probably bad karma, don't share this view or handle without using ViewData.
            ViewData["Title"] = "Closed Bugs";

            var closedBugs = await _context.Bugs
                // I recently started to prefer using `== false` to ! because it's more obvious
                .Where(bug => bug.IsClosed)
                .ToListAsync();
            
            return View("Index", closedBugs);
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

            return View(bug);
        }

        // GET: Bug/Create
        public IActionResult Create()
        {
            // TODO: if none found show warning on UI explaining how to add any.
            var peopleSelectListItems = _context.People
                .Select(person => new SelectListItem($"{person.Name} {person.Surname}", person.PersonId.ToString()))
                .ToList();

            var model = new BugCreateViewModel();
            model.PeopleSelectListItems = peopleSelectListItems;
            
            // Add default empty option
            model.PeopleSelectListItems.Insert(0, new SelectListItem("", null));

            // TODO: refactor this to view model.
            return View(model);
        }

        // POST: Bug/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Created date will not be bound because it needs to default to UTC Now.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BugId,Title,Description,IsClosed")] BugCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Bug/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bugs.FindAsync(id);
            if (bug == null)
            {
                return NotFound();
            }
            return View(bug);
        }

        // POST: Bug/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BugId,Created,Title,Description,IsClosed")] Bug bug)
        {
            if (id != bug.BugId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bug);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BugExists(bug.BugId))
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
            return View(bug);
        }

        // GET: Bug/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            return View(bug);
        }

        // POST: Bug/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bug = await _context.Bugs.FindAsync(id);
            _context.Bugs.Remove(bug);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BugExists(int id)
        {
            return _context.Bugs.Any(e => e.BugId == id);
        }
    }
}
