using KiemTraBuoi7.Models;

public interface INganhHocRepository
{
    Task<IEnumerable<NganhHoc>> GetAllAsync();
    Task<NganhHoc> GetByIdAsync(string id);
    Task AddAsync(NganhHoc nganhHoc);
    Task UpdateAsync(NganhHoc nganhHoc);
    Task DeleteAsync(string id);
}