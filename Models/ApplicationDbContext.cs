namespace KiemTraBuoi7.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<SinhVien> SinhViens { get; set; }
    public DbSet<NganhHoc> NganhHocs { get; set; }
    public DbSet<HocPhan> HocPhans { get; set; }
    public DbSet<DangKy> DangKies { get; set; }
    public DbSet<ChiTietDangKy> ChiTietDangKies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Cấu hình khóa chính composite cho ChiTietDangKy
        modelBuilder.Entity<ChiTietDangKy>()
            .HasKey(ctdk => new { ctdk.Madk, ctdk.Mahp });

        // Cấu hình cascade delete cho DangKy khi xóa SinhVien
        modelBuilder.Entity<DangKy>()
            .HasOne(dk => dk.SinhVien)
            .WithMany()
            .HasForeignKey(dk => dk.Masv)
            .OnDelete(DeleteBehavior.Cascade);

        // Cấu hình cascade delete cho ChiTietDangKy khi xóa DangKy
        modelBuilder.Entity<ChiTietDangKy>()
            .HasOne(ctdk => ctdk.DangKy)
            .WithMany()
            .HasForeignKey(ctdk => ctdk.Madk)
            .OnDelete(DeleteBehavior.Cascade);

        // Seed data cho NganhHoc
        modelBuilder.Entity<NganhHoc>().HasData(
            new NganhHoc { MaNganh = "CNTT", TenNganh = "Công nghệ thông tin" },
            new NganhHoc { MaNganh = "QTKD", TenNganh = "Quản trị kinh doanh" }
        );

        // Seed data cho SinhVien
        modelBuilder.Entity<SinhVien>().HasData(
            new SinhVien { Masv = "0123456789", HoTen = "Nguyễn Văn A", GioiTinh = "Nam", NgaySinh = new DateTime(2000, 12, 2), Hinh = "/Content/images/sv1.jpg", MaNganh = "CNTT" },
            new SinhVien { Masv = "9876543210", HoTen = "Nguyễn Thị B", GioiTinh = "Nữ", NgaySinh = new DateTime(2000, 3, 7), Hinh = "/Content/images/sv2.jpg", MaNganh = "QTKD" }
        );

        // Seed data cho HocPhan
        modelBuilder.Entity<HocPhan>().HasData(
            new HocPhan { Mahp = "CNTT01", TenHp = "Lập trình C", SoTinChi = 3 },
            new HocPhan { Mahp = "CNTT02", TenHp = "Cơ sở dữ liệu", SoTinChi = 2 },
            new HocPhan { Mahp = "QTKD01", TenHp = "Khởi sự và thương kệ", SoTinChi = 2 },
            new HocPhan { Mahp = "QTKD02", TenHp = "Xác suất thống kê", SoTinChi = 3 }
        );

        // Seed data cho DangKy
        modelBuilder.Entity<DangKy>().HasData(
            new DangKy { Madk = 1, NgayDk = new DateTime(2025, 6, 1), Masv = "0123456789" },
            new DangKy { Madk = 2, NgayDk = new DateTime(2025, 6, 1), Masv = "9876543210" }
        );

        // Seed data cho ChiTietDangKy
        modelBuilder.Entity<ChiTietDangKy>().HasData(
            new ChiTietDangKy { Madk = 1, Mahp = "CNTT01" },
            new ChiTietDangKy { Madk = 1, Mahp = "CNTT02" },
            new ChiTietDangKy { Madk = 2, Mahp = "QTKD01" },
            new ChiTietDangKy { Madk = 2, Mahp = "QTKD02" }
        );
    }
}