using RapPhim1.Data;
using RapPhim1.Models;
using Microsoft.EntityFrameworkCore;


namespace RapPhim1.DAO
{
    public class SeatDAO
    {
        private readonly AppDbContext _context;
        public SeatDAO(AppDbContext context) => _context = context;

        public async Task<List<Seat>> GetAllAsync() => await _context.Seats.Include(s => s.SeatType).ToListAsync();
        public async Task<Seat?> GetByIdAsync(int id) => await _context.Seats.Include(s => s.SeatType).FirstOrDefaultAsync(s => s.Id == id);
        public async Task AddAsync(Seat seat) => await _context.Seats.AddAsync(seat);
        public void Update(Seat seat) => _context.Seats.Update(seat);
        public void Delete(Seat seat) => seat.IsActive = false;
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
