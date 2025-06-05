using KiemTraBuoi7.Models;

public interface ISinhVienRepository
{
    Task<IEnumerable<SinhVien>> GetAllAsync();
    Task<SinhVien> GetByIdAsync(string id);
    Task AddAsync(SinhVien sinhVien);
    Task UpdateAsync(SinhVien sinhVien);
    Task DeleteAsync(string id);
}