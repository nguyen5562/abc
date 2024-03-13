using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopPet.Models;
using ShopPet.Repository;
using X.PagedList;

namespace ShopPet.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClaimsController : Controller
    {
        private readonly DataContext _context;

        public ClaimsController(DataContext context)
        {
            _context = context;
        }

        // GET: Admin/Claims
        public async Task<IActionResult> Index(int page = 1, int pageSize = 6)
        {
            var query = await _context.Claims.ToListAsync();

            var totalItemCount = query.Count();
            var model = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var pagedList = new StaticPagedList<Claims>(model, page, pageSize, totalItemCount);
            ViewBag.PageStartItem = (page - 1) * pageSize + 1;
            ViewBag.PageEndItem = Math.Min(page * pageSize, totalItemCount);
            ViewBag.TotalItemCount = totalItemCount;
            ViewBag.Page = page;
            return View(pagedList);
        }

        // GET: Admin/Claims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Claims == null)
            {
                return NotFound();
            }

            var claims = await _context.Claims
                .FirstOrDefaultAsync(m => m.Id == id);
            if (claims == null)
            {
                return NotFound();
            }

            return View(claims);
        }

        // GET: Admin/Claims/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Claims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClaimName")] Claims claims)
        {
            if (ModelState.IsValid)
            {
                _context.Add(claims);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(claims);
        }

        // GET: Admin/Claims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Claims == null)
            {
                return NotFound();
            }

            var claims = await _context.Claims.FindAsync(id);
            if (claims == null)
            {
                return NotFound();
            }
            return View(claims);
        }

        // POST: Admin/Claims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClaimName")] Claims claims)
        {
            if (id != claims.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(claims);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaimsExists(claims.Id))
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
            return View(claims);
        }

        // GET: Admin/Claims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Claims == null)
            {
                return NotFound();
            }

            var claims = await _context.Claims
                .FirstOrDefaultAsync(m => m.Id == id);
            if (claims == null)
            {
                return NotFound();
            }

            return View(claims);
        }

        // POST: Admin/Claims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Claims == null)
            {
                return Problem("Entity set 'DataContext.Claims'  is null.");
            }
            var claims = await _context.Claims.FindAsync(id);
            if (claims != null)
            {
                _context.Claims.Remove(claims);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClaimsExists(int id)
        {
          return _context.Claims.Any(e => e.Id == id);
        }
    }
}
