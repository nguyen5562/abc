using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopPet.Models;
using ShopPet.Models.ViewModels;
using ShopPet.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;

namespace ShopPet.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SupplierController : Controller
    {
        private readonly DataContext _context;

        public SupplierController(DataContext context)
        {
            _context = context;
        }

        // GET: Admin/Supplier
        public async Task<IActionResult> Index(string ten, string dc, string sdt, int page = 1, int pageSize = 5)
        {
            var query = _context.NhaCungCaps.ToList();

            if (!string.IsNullOrEmpty(ten))
                query = query.Where(h => h.TenNCC.Contains(ten, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(dc))
                query = query.Where(h => h.DiaChi.Contains(dc, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(sdt))
                query = query.Where(h => h.SĐT.Contains(sdt, StringComparison.OrdinalIgnoreCase)).ToList();

            var totalItemCount = query.Count();
            var model = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var pagedList = new StaticPagedList<NhaCungCap>(model, page, pageSize, totalItemCount);
            ViewBag.PageStartItem = (page - 1) * pageSize + 1;
            ViewBag.PageEndItem = Math.Min(page * pageSize, totalItemCount);
            ViewBag.TotalItemCount = totalItemCount;
            ViewBag.Page = page;
            ViewBag.ten = ten;
            ViewBag.dc = dc;
            ViewBag.sdt = sdt;
            return View(pagedList);
        }

        // GET: Admin/Supplier/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NhaCungCaps == null)
            {
                return NotFound();
            }

            var nhaCungCap = await _context.NhaCungCaps
                .FirstOrDefaultAsync(m => m.MaNCC == id);
            if (nhaCungCap == null)
            {
                return NotFound();
            }

            return View(nhaCungCap);
        }

        // GET: Admin/Supplier/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNCC,TenNCC,DiaChi,SĐT")] NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhaCungCap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhaCungCap);
        }

        // GET: Admin/Supplier/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NhaCungCaps == null)
            {
                return NotFound();
            }

            var nhaCungCap = await _context.NhaCungCaps.FindAsync(id);
            if (nhaCungCap == null)
            {
                return NotFound();
            }
            return View(nhaCungCap);
        }

        // POST: Admin/Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaNCC,TenNCC,DiaChi,SĐT")] NhaCungCap nhaCungCap)
        {
            if (id != nhaCungCap.MaNCC)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhaCungCap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhaCungCapExists(nhaCungCap.MaNCC))
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
            return View(nhaCungCap);
        }

        // GET: Admin/Supplier/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NhaCungCaps == null)
            {
                return NotFound();
            }

            var nhaCungCap = await _context.NhaCungCaps
                .FirstOrDefaultAsync(m => m.MaNCC == id);
            if (nhaCungCap == null)
            {
                return NotFound();
            }

            _context.Remove(nhaCungCap);
            _context.SaveChanges();
            return RedirectToAction("Index", "Supplier");
        }

        private bool NhaCungCapExists(int id)
        {
          return _context.NhaCungCaps.Any(e => e.MaNCC == id);
        }
    }
}
