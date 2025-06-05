using KiemTraBuoi7.Models;
using Microsoft.EntityFrameworkCore;

public class EFChiTietDangKyRepository : IChiTietDangKyRepository
{
    private readonly ApplicationDbContext _context;

    public EFChiTietDangKyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ChiTietDangKy>> GetByDangKyAsync(int madk)
    {
        return await _context.ChiTietDangKies
            .Include(ctdk => ctdk.HocPhan)
            .Where(ctdk => ctdk.Madk == madk)
            .ToListAsync();
    }

    public async Task AddAsync(ChiTietDangKy chiTietDangKy)
    {
        _context.ChiTietDangKies.Add(chiTietDangKy);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int madk, string mahp)
    {
        var chiTietDangKy = await _context.ChiTietDangKies
            .FirstOrDefaultAsync(ctdk => ctdk.Madk == madk && ctdk.Mahp == mahp);
        if (chiTietDangKy != null)
        {
            _context.ChiTietDangKies.Remove(chiTietDangKy);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAllByDangKyAsync(int madk)
    {
        var chiTietDangKys = await _context.ChiTietDangKies
            .Where(ctdk => ctdk.Madk == madk)
            .ToListAsync();
        _context.ChiTietDangKies.RemoveRange(chiTietDangKys);
        await _context.SaveChangesAsync();
    }
}