using KiemTraBuoi7.Models;
using Microsoft.EntityFrameworkCore;

public class EFHocPhanRepository : IHocPhanRepository
{
    private readonly ApplicationDbContext _context;

    public EFHocPhanRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<HocPhan>> GetAllAsync()
    {
        return await _context.HocPhans.ToListAsync();
    }

    public async Task<HocPhan> GetByIdAsync(string id)
    {
        return await _context.HocPhans.FindAsync(id);
    }
}