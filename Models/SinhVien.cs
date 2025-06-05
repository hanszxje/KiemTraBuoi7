namespace KiemTraBuoi7.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class SinhVien
{
    [Key]
    [StringLength(10)]
    [Display(Name = "Mã sinh viên")]
    public string Masv { get; set; }

    [Required(ErrorMessage = "Họ tên là bắt buộc")]
    [StringLength(50, ErrorMessage = "Họ tên không được vượt quá 50 ký tự")]
    [Display(Name = "Họ tên")]
    public string HoTen { get; set; }

    [StringLength(5)]
    [Display(Name = "Giới tính")]
    public string GioiTinh { get; set; }

    [Display(Name = "Ngày sinh")]
    public DateTime? NgaySinh { get; set; }

    [StringLength(50)]
    [Display(Name = "Hình ảnh")]
    public string? Hinh { get; set; }

    [ForeignKey("NganhHoc")]
    [StringLength(4)]
    [Display(Name = "Mã ngành")]
    public string MaNganh { get; set; }

    [Display(Name = "Ngành học")]
    public NganhHoc? MaNganhNavigation { get; set; }
}