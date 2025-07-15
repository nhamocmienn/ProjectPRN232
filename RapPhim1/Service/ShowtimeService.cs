using RapPhim1.DAO;
using RapPhim1.DTO;
using RapPhim1.DTO.Admin.showtimes;
using RapPhim1.Models;

namespace RapPhim1.Service
{
    public class ShowtimeService : IShowtimeService
    {
        private readonly ShowtimeDAO _dao;
        public ShowtimeService(ShowtimeDAO dao) => _dao = dao;

        public async Task<List<ShowtimeDTO>> GetAllAsync()
        {
            var list = await _dao.GetAllAsync();

            return list.Select(s => new ShowtimeDTO
            {
                Id = s.Id,
                MovieId = s.MovieId,
                MovieTitle = s.Movie.Title,
                RoomId = s.RoomId,
                RoomName = s.Room.Name,
                StartTime = s.StartTime,
                IsActive = s.IsActive
            }).ToList();
        }

        public async Task<ShowtimeDTO?> GetByIdAsync(int id)
        {
            var s = await _dao.GetByIdAsync(id);
            if (s == null) return null;

            return new ShowtimeDTO
            {
                Id = s.Id,
                MovieId = s.MovieId,
                MovieTitle = s.Movie.Title,
                RoomId = s.RoomId,
                RoomName = s.Room.Name,
                StartTime = s.StartTime,
                IsActive = s.IsActive
            };
        }

        public async Task AddAsync(Showtime showtime)
        {
            await _dao.AddAsync(showtime);
            await _dao.SaveChangesAsync();
        }

        public async Task UpdateAsync(Showtime showtime)
        {
            _dao.Update(showtime);
            await _dao.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var showtime = await _dao.GetByIdAsync(id);
            if (showtime != null)
            {
                _dao.Delete(showtime);
                await _dao.SaveChangesAsync();
            }
        }

        public async Task<Showtime?> GetEntityByIdAsync(int id)
    => await _dao.GetByIdAsync(id);

        public async Task<bool> IsConflictAsync(int roomId, DateTime startTime, int duration, int? ignoreId = null)
            => await _dao.IsConflictAsync(roomId, startTime, duration, ignoreId);
    }

    public interface IShowtimeService
    {
        Task<List<ShowtimeDTO>> GetAllAsync();
        Task<Showtime?> GetEntityByIdAsync(int id);
        Task<ShowtimeDTO?> GetByIdAsync(int id);
        Task AddAsync(Showtime showtime);
        Task UpdateAsync(Showtime showtime);
        Task DeleteAsync(int id);
        Task<bool> IsConflictAsync(int roomId, DateTime startTime, int duration, int? ignoreId = null);
    }
}
