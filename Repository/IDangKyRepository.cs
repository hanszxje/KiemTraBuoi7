using KiemTraBuoi7.Models;

public interface IDangKyRepository
{
    Task<DangKy> GetBySinhVienAsync(string masv);
    Task AddAsync(DangKy dangKy);
    Task DeleteAsync(int id);
}