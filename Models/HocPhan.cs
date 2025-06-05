namespace KiemTraBuoi7.Models;
using System.ComponentModel.DataAnnotations;

public class HocPhan
{
    [Key]
    [StringLength(6)]
    [Display(Name = "Mã học phần")]
    public string Mahp { get; set; }

    [Required(ErrorMessage = "Tên học phần là bắt buộc")]
    [StringLength(30, ErrorMessage = "Tên học phần không được vượt quá 30 ký tự")]
    [Display(Name = "Tên học phần")]
    public string TenHp { get; set; }

    [Range(1, 10, ErrorMessage = "Số tín chỉ phải từ 1 đến 10")]
    [Display(Name = "Số tín chỉ")]
    public int SoTinChi { get; set; }
}