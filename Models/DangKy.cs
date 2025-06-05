
namespace KiemTraBuoi7.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class DangKy
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "Mã đăng ký")]
    public int Madk { get; set; }

    [Display(Name = "Ngày đăng ký")]
    public DateTime? NgayDk { get; set; }

    [StringLength(10)]
    [ForeignKey("SinhVien")]
    [Display(Name = "Mã sinh viên")]
    public string Masv { get; set; }

    public SinhVien? SinhVien { get; set; }
}