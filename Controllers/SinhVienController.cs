using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KiemTraBuoi7.Models;
using Microsoft.AspNetCore.Authorization;

namespace KiemTraBuoi7.Controllers
{
    [Authorize(Roles = "Admin")] // Chỉ admin được truy cập
    public class SinhVienController : Controller
    {
        private readonly ISinhVienRepository _sinhVienRepository;
        private readonly INganhHocRepository _nganhHocRepository;

        public SinhVienController(ISinhVienRepository sinhVienRepository,
                                  INganhHocRepository nganhHocRepository)
        {
            _sinhVienRepository = sinhVienRepository;
            _nganhHocRepository = nganhHocRepository;
        }

        public async Task<IActionResult> Index()
        {
            var sinhViens = await _sinhVienRepository.GetAllAsync();
            return View(sinhViens);
        }

        public async Task<IActionResult> Create()
        {
            var nganhHocs = await _nganhHocRepository.GetAllAsync();
            ViewBag.NganhHocs = new SelectList(nganhHocs, "MaNganh", "TenNganh");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SinhVien sinhVien, IFormFile hinh)
        {
            if (ModelState.IsValid)
            {
                if (hinh != null)
                {
                    var savePath = Path.Combine("wwwroot/images", hinh.FileName);
                    using (var fileStream = new FileStream(savePath, FileMode.Create))
                    {
                        await hinh.CopyToAsync(fileStream);
                    }
                    sinhVien.Hinh = "/images/" + hinh.FileName;
                }

                await _sinhVienRepository.AddAsync(sinhVien);
                return RedirectToAction(nameof(Index));
            }

            var nganhHocs = await _nganhHocRepository.GetAllAsync();
            ViewBag.NganhHocs = new SelectList(nganhHocs, "MaNganh", "TenNganh");
            return View(sinhVien);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var sinhVien = await _sinhVienRepository.GetByIdAsync(id);
            if (sinhVien == null)
            {
                return NotFound();
            }

            var nganhHocs = await _nganhHocRepository.GetAllAsync();
            ViewBag.NganhHocs = new SelectList(nganhHocs, "MaNganh", "TenNganh", sinhVien.MaNganh);
            return View(sinhVien);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, SinhVien sinhVien, IFormFile hinh)
        {
            if (id != sinhVien.Masv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (hinh != null)
                    {
                        var savePath = Path.Combine("wwwroot/images", hinh.FileName);
                        using (var fileStream = new FileStream(savePath, FileMode.Create))
                        {
                            await hinh.CopyToAsync(fileStream);
                        }
                        sinhVien.Hinh = "/images/" + hinh.FileName;
                    }
                    else
                    {
                        var existingSinhVien = await _sinhVienRepository.GetByIdAsync(id);
                        sinhVien.Hinh = existingSinhVien.Hinh;
                    }

                    await _sinhVienRepository.UpdateAsync(sinhVien);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _sinhVienRepository.GetByIdAsync(id) == null)
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

            var nganhHocs = await _nganhHocRepository.GetAllAsync();
            ViewBag.NganhHocs = new SelectList(nganhHocs, "MaNganh", "TenNganh", sinhVien.MaNganh);
            return View(sinhVien);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var sinhVien = await _sinhVienRepository.GetByIdAsync(id);
            if (sinhVien == null)
            {
                return NotFound();
            }
            return View(sinhVien);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                await _sinhVienRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Xóa sinh viên thành công.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi khi xóa sinh viên: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            var sinhVien = await _sinhVienRepository.GetByIdAsync(id);
            if (sinhVien == null)
            {
                return NotFound();
            }
            return View(sinhVien);
        }
    }
}