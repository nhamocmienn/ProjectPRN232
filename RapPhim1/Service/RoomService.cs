using RapPhim1.DAO;
using RapPhim1.Models;

namespace RapPhim1.Service
{
    public class RoomService : IRoomService
    {
        private readonly RoomDAO _roomDAO;
        public RoomService(RoomDAO roomDAO) => _roomDAO = roomDAO;

        public async Task<List<Room>> GetAllAsync() => await _roomDAO.GetAllAsync();
        public async Task<Room?> GetByIdAsync(int id) => await _roomDAO.GetByIdAsync(id);
        public async Task AddAsync(Room room)
        {
            await _roomDAO.AddAsync(room);
            await _roomDAO.SaveChangesAsync();
        }
        public async Task UpdateAsync(Room room)
        {
            _roomDAO.Update(room);
            await _roomDAO.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var room = await _roomDAO.GetByIdAsync(id);
            if (room != null)
            {
                _roomDAO.Delete(room);
                await _roomDAO.SaveChangesAsync();
            }
        }
    }
}
