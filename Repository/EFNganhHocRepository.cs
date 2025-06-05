using KiemTraBuoi7.Models;
using Microsoft.EntityFrameworkCore;

public class EFNganhHocRepository : INganhHocRepository
{
    private readonly ApplicationDbContext _context;

    public EFNganhHocRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<NganhHoc>> GetAllAsync()
    {
        return await _context.NganhHocs.ToListAsync();
    }

    public async Task<NganhHoc> GetByIdAsync(string id)
    {
        return await _context.NganhHocs.SingleOrDefaultAsync(x => x.MaNganh == id);
    }

    public async Task AddAsync(NganhHoc nganhHoc)
    {
        _context.NganhHocs.Add(nganhHoc);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(NganhHoc nganhHoc)
    {
        _context.NganhHocs.Update(nganhHoc);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var nganhHoc = await _context.NganhHocs.FindAsync(id);
        _context.NganhHocs.Remove(nganhHoc);
        await _context.SaveChangesAsync();
    }
}