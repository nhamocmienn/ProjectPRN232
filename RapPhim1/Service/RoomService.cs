using RapPhim1.DAO;
using RapPhim1.DTO.Admin.room;
using RapPhim1.DTO.Admin.seat;
using RapPhim1.Models;

namespace RapPhim1.Service
{
    public class RoomService : IRoomService
    {
        private readonly RoomDAO _roomDAO;
        public RoomService(RoomDAO roomDAO) => _roomDAO = roomDAO;

        public async Task<List<RoomDTO>> GetAllAsync()
        {
            var rooms = await _roomDAO.GetAllAsync();
            return rooms.Select(r => new RoomDTO
            {
                Id = r.Id,
                Name = r.Name,
                IsActive = r.IsActive,
                Seats = r.Seats.Select(s => new SeatDTO
                {
                    Id = s.Id,
                    RoomId = s.RoomId,
                    Row = s.Row,
                    Column = s.Column,
                    SeatTypeId = s.SeatTypeId,
                    SeatTypeName = s.SeatType?.Name ?? string.Empty,  // Null check
                    ExtraFee = s.SeatType?.ExtraFee ?? 0              // Null check
                }).ToList()
            }).ToList();
        }
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
