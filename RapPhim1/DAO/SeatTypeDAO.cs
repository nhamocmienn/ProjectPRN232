using RapPhim1.Data;
using RapPhim1.Models;
using Microsoft.EntityFrameworkCore;
namespace RapPhim1.DAO
{
        public class SeatTypeDAO
        {
            private readonly AppDbContext _context;
            public SeatTypeDAO(AppDbContext context) => _context = context;

            public async Task<List<SeatType>> GetAllAsync() =>
                await _context.SeatTypes
                    .Where(st => st.IsActive)
                    .ToListAsync();

            public async Task<SeatType?> GetByIdAsync(int id) =>
                await _context.SeatTypes
                    .FirstOrDefaultAsync(st => st.Id == id && st.IsActive);

            public async Task AddAsync(SeatType seatType) =>
                await _context.SeatTypes.AddAsync(seatType);

            public void Update(SeatType seatType) =>
                _context.SeatTypes.Update(seatType);

            public void Delete(SeatType seatType) =>
                seatType.IsActive = false;

            public async Task SaveChangesAsync() =>
                await _context.SaveChangesAsync();
        }
    
}
