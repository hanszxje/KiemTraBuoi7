using KiemTraBuoi7.Models;

public interface IHocPhanRepository
{
    Task<IEnumerable<HocPhan>> GetAllAsync();
    Task<HocPhan> GetByIdAsync(string id);
}