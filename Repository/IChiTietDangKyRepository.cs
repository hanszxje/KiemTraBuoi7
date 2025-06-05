using KiemTraBuoi7.Models;

public interface IChiTietDangKyRepository
{
    Task<IEnumerable<ChiTietDangKy>> GetByDangKyAsync(int madk);
    Task AddAsync(ChiTietDangKy chiTietDangKy);
    Task DeleteAsync(int madk, string mahp);
    Task DeleteAllByDangKyAsync(int madk);
}