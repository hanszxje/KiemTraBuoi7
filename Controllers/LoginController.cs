using Microsoft.AspNetCore.Mvc;
using KiemTraBuoi7.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;

public class LoginController : Controller
{
    private readonly ISinhVienRepository _sinhVienRepository;

    public LoginController(ISinhVienRepository sinhVienRepository)
    {
        _sinhVienRepository = sinhVienRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(string maSV)
    {
        if (string.IsNullOrEmpty(maSV?.Trim()))
        {
            ViewBag.ErrorMessage = "Vui lòng nhập mã sinh viên hợp lệ.";
            return View();
        }

        var sinhVien = await _sinhVienRepository.GetByIdAsync(maSV.Trim());
        if (sinhVien == null)
        {
            ViewBag.ErrorMessage = "Mã sinh viên không tồn tại.";
            return View();
        }

        // Tạo claims cho sinh viên hoặc admin
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, sinhVien.Masv),
            new Claim("FullName", sinhVien.HoTen),
            new Claim(ClaimTypes.Role, maSV.Trim().ToLower() == "admin" ? "Admin" : "SinhVien")
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true // Giữ đăng nhập sau khi đóng trình duyệt
        };

        // Đăng nhập bằng cookie authentication
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity), authProperties);

        // Chuyển hướng dựa trên vai trò
        if (maSV.Trim().ToLower() == "admin")
        {
            return RedirectToAction("Index", "SinhVien");
        }
        else
        {
            return RedirectToAction("DaDangKy", "DangKy");
        }
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Login");
    }
}