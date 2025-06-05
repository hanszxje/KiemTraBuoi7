using KiemTraBuoi7.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Cấu hình Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";
        options.LogoutPath = "/Login/Logout";
        options.AccessDeniedPath = "/DangKy/DaDangKy"; // Chuyển hướng đến trang Đăng Kí Học Phần nếu không có quyền
    });

// Cấu hình DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext")));

// Cấu hình DI cho Repository
builder.Services.AddScoped<ISinhVienRepository, EFSinhVienRepository>();
builder.Services.AddScoped<INganhHocRepository, EFNganhHocRepository>();
builder.Services.AddScoped<IHocPhanRepository, EFHocPhanRepository>();
builder.Services.AddScoped<IDangKyRepository, EFDangKyRepository>();
builder.Services.AddScoped<IChiTietDangKyRepository, EFChiTietDangKyRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Thêm middleware Authentication
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}"); // Đổi controller mặc định thành Login

app.Run();