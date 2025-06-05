using Microsoft.AspNetCore.Mvc;
using KiemTraBuoi7.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace KiemTraBuoi7.Controllers
{
    [Authorize(Roles = "SinhVien")]
    public class DangKyController : Controller
    {
        private readonly IHocPhanRepository _hocPhanRepository;
        private readonly IDangKyRepository _dangKyRepository;
        private readonly IChiTietDangKyRepository _chiTietDangKyRepository;
        private readonly ISinhVienRepository _sinhVienRepository;

        public DangKyController(
            IHocPhanRepository hocPhanRepository,
            IDangKyRepository dangKyRepository,
            IChiTietDangKyRepository chiTietDangKyRepository,
            ISinhVienRepository sinhVienRepository)
        {
            _hocPhanRepository = hocPhanRepository;
            _dangKyRepository = dangKyRepository;
            _chiTietDangKyRepository = chiTietDangKyRepository;
            _sinhVienRepository = sinhVienRepository;
        }

        // Hiển thị danh sách học phần để đăng ký
        public async Task<IActionResult> Index()
        {
            var hocPhans = await _hocPhanRepository.GetAllAsync();

            // Lấy mã sinh viên từ User
            var masv = User.FindFirst(ClaimTypes.Name)?.Value;
            if (!string.IsNullOrEmpty(masv))
            {
                var dangKy = await _dangKyRepository.GetBySinhVienAsync(masv);
                if (dangKy != null)
                {
                    var chiTietDangKys = await _chiTietDangKyRepository.GetByDangKyAsync(dangKy.Madk);
                    // Truyền danh sách mã học phần đã đăng ký vào ViewBag
                    ViewBag.DaDangKy = chiTietDangKys.Select(ctdk => ctdk.Mahp).ToList();
                }
                else
                {
                    ViewBag.DaDangKy = new List<string>();
                }
            }
            else
            {
                ViewBag.DaDangKy = new List<string>();
            }

            return View(hocPhans);
        }

        // Đăng ký học phần
        [HttpPost]
        public async Task<IActionResult> Register(string mahp)
        {
            var masv = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(masv))
            {
                return RedirectToAction("Index", "Login");
            }

            // Kiểm tra xem sinh viên đã có đăng ký chưa
            var dangKy = await _dangKyRepository.GetBySinhVienAsync(masv);
            if (dangKy == null)
            {
                dangKy = new DangKy
                {
                    Masv = masv,
                    NgayDk = DateTime.Now
                };
                await _dangKyRepository.AddAsync(dangKy);
            }

            // Kiểm tra xem học phần đã được đăng ký chưa
            var chiTietDangKys = await _chiTietDangKyRepository.GetByDangKyAsync(dangKy.Madk);
            if (chiTietDangKys.Any(ctdk => ctdk.Mahp == mahp))
            {
                TempData["ErrorMessage"] = "Học phần này đã được đăng ký.";
                return RedirectToAction(nameof(Index));
            }

            // Thêm học phần vào ChiTietDangKy
            var chiTietDangKy = new ChiTietDangKy
            {
                Madk = dangKy.Madk,
                Mahp = mahp
            };
            await _chiTietDangKyRepository.AddAsync(chiTietDangKy);

            TempData["SuccessMessage"] = "Đăng ký học phần thành công.";
            return RedirectToAction(nameof(Index));
        }

        // Hiển thị học phần đã đăng ký
        public async Task<IActionResult> DaDangKy()
        {
            var masv = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(masv))
            {
                return RedirectToAction("Index", "Login");
            }

            // Lấy thông tin sinh viên
            var sinhVien = await _sinhVienRepository.GetByIdAsync(masv);
            if (sinhVien != null)
            {
                ViewBag.MaSV = sinhVien.Masv;
                ViewBag.HoTen = sinhVien.HoTen;
                ViewBag.NgaySinh = sinhVien.NgaySinh?.ToString("dd/MM/yyyy");
                ViewBag.Nganh = sinhVien.MaNganhNavigation?.TenNganh ?? "Không có ngành";
            }
            else
            {
                ViewBag.MaSV = masv;
                ViewBag.HoTen = User.FindFirst("FullName")?.Value;
                ViewBag.NgaySinh = "Không có thông tin";
                ViewBag.Nganh = "Không có thông tin";
            }

            var dangKy = await _dangKyRepository.GetBySinhVienAsync(masv);
            if (dangKy == null)
            {
                ViewBag.SoHocPhan = 0;
                ViewBag.TongSoTinChi = 0;
                return View(new List<ChiTietDangKy>());
            }

            var chiTietDangKys = await _chiTietDangKyRepository.GetByDangKyAsync(dangKy.Madk);
            ViewBag.SoHocPhan = chiTietDangKys.Count();
            ViewBag.TongSoTinChi = chiTietDangKys.Sum(ctdk => ctdk.HocPhan?.SoTinChi ?? 0);

            return View(chiTietDangKys);
        }

        // Xóa một học phần đã đăng ký
        [HttpPost]
        public async Task<IActionResult> XoaDangKy(int madk, string mahp)
        {
            await _chiTietDangKyRepository.DeleteAsync(madk, mahp);
            TempData["SuccessMessage"] = "Xóa học phần thành công.";
            return RedirectToAction(nameof(DaDangKy));
        }

        // Xóa tất cả học phần đã đăng ký
        [HttpPost]
        public async Task<IActionResult> XoaTatCaDangKy()
        {
            var masv = User.FindFirst(ClaimTypes.Name)?.Value;
            var dangKy = await _dangKyRepository.GetBySinhVienAsync(masv);
            if (dangKy != null)
            {
                await _chiTietDangKyRepository.DeleteAllByDangKyAsync(dangKy.Madk);
                await _dangKyRepository.DeleteAsync(dangKy.Madk);
                TempData["SuccessMessage"] = "Xóa tất cả đăng ký thành công.";
            }
            return RedirectToAction(nameof(DaDangKy));
        }
    }
}