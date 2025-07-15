using Microsoft.EntityFrameworkCore;
using RapPhim1.Data;
using RapPhim1.Models;

namespace RapPhim1.DAO
{
    public class ShowtimeDAO
    {
        private readonly AppDbContext _context;
        public ShowtimeDAO(AppDbContext context) => _context = context;

        public async Task<List<Showtime>> GetAllAsync() =>
          await _context.Showtimes
         //.Where(s => s.IsActive)
         .Include(s => s.Movie)
         .Include(s => s.Room)
         .ToListAsync();

        public async Task<Showtime?> GetByIdAsync(int id) =>
            await _context.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Room)
                .FirstOrDefaultAsync(s => s.Id == id);
        public async Task AddAsync(Showtime showtime) =>
            await _context.Showtimes.AddAsync(showtime);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Update(Showtime showtime) => _context.Showtimes.Update(showtime);

        public void Delete(Showtime showtime) => showtime.IsActive = false;

        public async Task<bool> IsConflictAsync(int roomId, DateTime startTime, int duration, int? ignoreShowtimeId = null)
        {
            var endTime = startTime.AddMinutes(duration + 30);

            return await _context.Showtimes
                .Include(s => s.Movie)
                .Where(s => s.RoomId == roomId && s.IsActive)
                .Where(s => ignoreShowtimeId == null || s.Id != ignoreShowtimeId.Value)
                .AnyAsync(s =>
                    startTime < s.StartTime.AddMinutes(s.Movie.Duration + 30) &&
                    endTime > s.StartTime
                );
        }
    }
}
