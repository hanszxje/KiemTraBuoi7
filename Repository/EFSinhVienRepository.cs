using KiemTraBuoi7.Models;
using Microsoft.EntityFrameworkCore;

public class EFSinhVienRepository : ISinhVienRepository
{
    private readonly ApplicationDbContext _context;

    public EFSinhVienRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SinhVien>> GetAllAsync()
    {
        return await _context.SinhViens.Include(x => x.MaNganhNavigation).ToListAsync();
    }

    public async Task<SinhVien> GetByIdAsync(string id)
    {
        return await _context.SinhViens.Include(x => x.MaNganhNavigation).SingleOrDefaultAsync(x => x.Masv == id);
    }

    public async Task AddAsync(SinhVien sinhVien)
    {
        _context.SinhViens.Add(sinhVien);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(SinhVien sinhVien)
    {
        _context.SinhViens.Update(sinhVien);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var sinhVien = await _context.SinhViens.FindAsync(id);
        if (sinhVien == null)
        {
            throw new Exception("Sinh viên không tồn tại.");
        }

        _context.SinhViens.Remove(sinhVien);
        await _context.SaveChangesAsync();
    }
}