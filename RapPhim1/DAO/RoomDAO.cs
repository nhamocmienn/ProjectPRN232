using RapPhim1.Data;
using RapPhim1.Models;
using Microsoft.EntityFrameworkCore;


namespace RapPhim1.DAO
{
    public class RoomDAO
    {
        private readonly AppDbContext _context;
        public RoomDAO(AppDbContext context) => _context = context;

        public async Task<List<Room>> GetAllAsync() => await _context.Rooms.Include(r => r.Seats).ThenInclude(s => s.SeatType).ToListAsync();
        public async Task<Room?> GetByIdAsync(int id) => await _context.Rooms.Include(r => r.Seats).ThenInclude(s => s.SeatType).FirstOrDefaultAsync(r => r.Id == id);
        public async Task AddAsync(Room room) => await _context.Rooms.AddAsync(room);
        public void Update(Room room) => _context.Rooms.Update(room);
        public void Delete(Room room) => room.IsActive = false;
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }

}
