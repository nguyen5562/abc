using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopPet.Models;
using ShopPet.Repository;
using X.PagedList;

namespace ShopPet.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly DataContext _context;

        public CategoryController(DataContext context)
        {
            _context = context;
        }

        // GET: Admin/Category
        public async Task<IActionResult> Index(string lsp, int page = 1, int pageSize = 5)
        {
            var query = _context.LoaiSanPhams.ToList();

            if (!string.IsNullOrEmpty(lsp))
                query = query.Where(h => h.TenLoaiSP.Contains(lsp, StringComparison.OrdinalIgnoreCase)).ToList();

            var totalItemCount = query.Count();
            var model = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var pagedList = new StaticPagedList<LoaiSanPham>(model, page, pageSize, totalItemCount);
            ViewBag.PageStartItem = (page - 1) * pageSize + 1;
            ViewBag.PageEndItem = Math.Min(page * pageSize, totalItemCount);
            ViewBag.TotalItemCount = totalItemCount;
            ViewBag.Page = page;
            ViewBag.lsp = lsp;
            return View(pagedList);
        }

        // GET: Admin/Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LoaiSanPhams == null)
            {
                return NotFound();
            }

            var loaiSanPham = await _context.LoaiSanPhams
                .FirstOrDefaultAsync(m => m.MaLoaiSP == id);
            if (loaiSanPham == null)
            {
                return NotFound();
            }

            return View(loaiSanPham);
        }

        // GET: Admin/Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[Authorize(Roles = "nhân viên")]*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLoaiSP,TenLoaiSP")] LoaiSanPham loaiSanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiSanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiSanPham);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LoaiSanPhams == null)
            {
                return NotFound();
            }

            var loaiSanPham = await _context.LoaiSanPhams.FindAsync(id);
            if (loaiSanPham == null)
            {
                return NotFound();
            }
            return View(loaiSanPham);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLoaiSP,TenLoaiSP")] LoaiSanPham loaiSanPham)
        {
            if (id != loaiSanPham.MaLoaiSP)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiSanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiSanPhamExists(loaiSanPham.MaLoaiSP))
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
            return View(loaiSanPham);
        }

        // GET: Admin/Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LoaiSanPhams == null)
            {
                return NotFound();
            }

            var loaiSanPham = await _context.LoaiSanPhams
                .FirstOrDefaultAsync(m => m.MaLoaiSP == id);
            if (loaiSanPham == null)
            {
                return NotFound();
            }

            _context.Remove(loaiSanPham);
            _context.SaveChanges();
            return RedirectToAction("Index", "Category");
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LoaiSanPhams == null)
            {
                return Problem("Entity set 'DataContext.LoaiSanPhams'  is null.");
            }
            var loaiSanPham = await _context.LoaiSanPhams.FindAsync(id);
            if (loaiSanPham != null)
            {
                _context.LoaiSanPhams.Remove(loaiSanPham);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiSanPhamExists(int id)
        {
          return _context.LoaiSanPhams.Any(e => e.MaLoaiSP == id);
        }
    }
}
