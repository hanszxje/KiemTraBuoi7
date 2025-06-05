namespace KiemTraBuoi7.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class SinhVienImage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Url { get; set; }

    [ForeignKey("SinhVien")]
    public string SinhVienId { get; set; }

    public SinhVien? SinhVien { get; set; }
}