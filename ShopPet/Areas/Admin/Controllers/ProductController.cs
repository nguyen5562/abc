using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopPet.Models;
using ShopPet.Models.ViewModels;
using ShopPet.Repository;
using System.Drawing.Printing;
using X.PagedList;

namespace ShopPet.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly DataContext _context;
		public ProductController(DataContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index(string searchTen, string lsp, string ncc, string loai, decimal minban, decimal maxban, decimal minnhap, decimal maxnhap, int page = 1, int pageSize = 5)
		{
            ViewBag.ListLoaiSp = await _context.LoaiSanPhams.ToListAsync();
            ViewBag.ListNcc = await _context.NhaCungCaps.ToListAsync();

            // Get data
            var query = from sp in _context.SanPhams
                        join nc in _context.NhaCungCaps on sp.NhaCungCapId equals nc.MaNCC
                        join ls in _context.LoaiSanPhams on sp.LoaiSanPhamId equals ls.MaLoaiSP
                        select new ProductViewModel()
                        {
                            MaSP = sp.MaSP,
                            TenSP = sp.TenSP,
                            MoTa = sp.MoTa,
                            GiaBan = sp.GiaBan,
                            GiaNhap = sp.GiaNhap,
                            Anh = sp.Anh,
                            SoLuongTon = sp.SoLuongTon,
                            MaLSP = ls.MaLoaiSP,
                            LoaiSanPham = ls.TenLoaiSP,
                            MaNCC = nc.MaNCC,
                            NhaCungCap = nc.TenNCC
                        };

            // Search
            if (!string.IsNullOrEmpty(searchTen))
                query = query.Where(s => s.TenSP.Contains(searchTen));

            if (!string.IsNullOrEmpty(lsp))
                query = query.Where(s => s.LoaiSanPham == lsp);

            if (!string.IsNullOrEmpty(ncc))
                query = query.Where(s => s.NhaCungCap == ncc);

            if (!string.IsNullOrEmpty(loai))
                query = query.Where(s => s.GiongLoai == loai);

            if (maxban != 0)
            {
                query = query.Where(item => item.GiaBan < maxban && item.GiaBan > minban);
            }

            if (maxnhap != 0)
            {
                query = query.Where(item => item.GiaNhap < maxnhap && item.GiaNhap > minnhap);
            }

            var totalItemCount = query.Count();
            var model = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var pagedList = new StaticPagedList<ProductViewModel>(model, page, pageSize, totalItemCount);
            ViewBag.PageStartItem = (page - 1) * pageSize + 1;
            ViewBag.PageEndItem = Math.Min(page * pageSize, totalItemCount);
            ViewBag.TotalItemCount = totalItemCount;
            ViewBag.Page = page;
            ViewBag.searchTen = searchTen;
            ViewBag.lsp = lsp;
            ViewBag.ncc = ncc;
            ViewBag.loai = loai;
            ViewBag.minban = minban;
            ViewBag.maxban = maxban;
            ViewBag.minnhap = minnhap;
            ViewBag.maxnhap = maxnhap;
            return View(pagedList);
        }

        public IActionResult Create()
        {           
            ViewBag.ListLoaiSp = _context.LoaiSanPhams.ToList();
            ViewBag.ListNcc = _context.NhaCungCaps.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SanPham sp, IFormFile image1, string LoaiSp, string Ncc)
        {
            var spmoi = new SanPham();
            spmoi.TenSP = sp.TenSP;
            spmoi.MoTa = sp.MoTa;
            spmoi.GiaNhap = sp.GiaNhap;
            spmoi.GiaBan = sp.GiaBan;
            spmoi.SoLuongTon = sp.SoLuongTon;

            if (image1 != null && image1.Length > 0)
            {
                string fileName = Path.GetFileName(image1.FileName);
                string uploadPath = Path.Combine("wwwroot", "Images", fileName);

                using (var fileStream = new FileStream(uploadPath, FileMode.Create))
                {
                    image1.CopyTo(fileStream);
                }

                spmoi.Anh = fileName;
            }

            var dm = _context.LoaiSanPhams.FirstOrDefault(s => s.TenLoaiSP == LoaiSp);
            if (dm != null)
            { 
                spmoi.LoaiSanPhamId = dm.MaLoaiSP;
            }

            var hang = _context.NhaCungCaps.FirstOrDefault(s => s.TenNCC == Ncc);
            if (hang != null)
            {
                spmoi.NhaCungCapId = hang.MaNCC;
            }
            _context.SanPhams.Add(spmoi);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int maSP)
        {
           
            SanPham sanPham = _context.SanPhams.Single(ma => ma.MaSP == maSP);
            if (sanPham == null)
            {
                return NotFound();
            }

            _context.SanPhams.Remove(sanPham);
            _context.SaveChanges();
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.ListLoaiSp = _context.LoaiSanPhams.ToList();
            ViewBag.ListNcc = _context.NhaCungCaps.ToList();
            SanPham product = _context.SanPhams.SingleOrDefault(n => n.MaSP.Equals(id));
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(SanPham sp, IFormFile image1)
        {
            SanPham product = _context.SanPhams.SingleOrDefault(n => n.MaSP == sp.MaSP);
            if (product == null)
            {
                return NotFound();
            }
            product.TenSP = sp.TenSP;
            product.MoTa = sp.MoTa;
            product.GiaBan = sp.GiaBan;
            product.GiaNhap = sp.GiaNhap;
            product.SoLuongTon = sp.SoLuongTon;
            product.Anh = sp.Anh;

            if (image1 != null) 
            {
                if (image1.Length > 0)
                {
                    var filename = Path.GetFileName(image1.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", filename);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        image1.CopyTo(stream);
                    }
                    product.Anh = image1.FileName;
                }
            }
            
            product.LoaiSanPhamId = sp.LoaiSanPhamId;
            product.NhaCungCapId = sp.NhaCungCapId;

            _context.SaveChanges();
            TempData["Edited"] = "Sửa thông tin sản phẩm thành công";
            return RedirectToAction("Index", "Product");
        }

    }
}

