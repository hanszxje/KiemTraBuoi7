namespace KiemTraBuoi7.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ChiTietDangKy
{
    [StringLength(6)]
    [ForeignKey("HocPhan")]
    [Display(Name = "Mã học phần")]
    public string Mahp { get; set; }

    [ForeignKey("DangKy")]
    [Display(Name = "Mã đăng ký")]
    public int Madk { get; set; }

    public HocPhan? HocPhan { get; set; }
    public DangKy? DangKy { get; set; }
}