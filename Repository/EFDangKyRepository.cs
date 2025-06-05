using KiemTraBuoi7.Models;
using Microsoft.EntityFrameworkCore;

public class EFDangKyRepository : IDangKyRepository
{
    private readonly ApplicationDbContext _context;

    public EFDangKyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DangKy> GetBySinhVienAsync(string masv)
    {
        return await _context.DangKies.FirstOrDefaultAsync(dk => dk.Masv == masv);
    }

    public async Task AddAsync(DangKy dangKy)
    {
        _context.DangKies.Add(dangKy);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var dangKy = await _context.DangKies.FindAsync(id);
        if (dangKy != null)
        {
            _context.DangKies.Remove(dangKy);
            await _context.SaveChangesAsync();
        }
    }
}