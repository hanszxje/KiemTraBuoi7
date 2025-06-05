namespace KiemTraBuoi7.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class NganhHoc
{
    [Key]
    [StringLength(4)]
    [Display(Name = "Mã ngành")]
    public string MaNganh { get; set; }

    [Required(ErrorMessage = "Tên ngành là bắt buộc")]
    [StringLength(30, ErrorMessage = "Tên ngành không được vượt quá 30 ký tự")]
    [Display(Name = "Tên ngành")]
    public string TenNganh { get; set; }

    public List<SinhVien> SinhViens { get; set; } = new List<SinhVien>();
}