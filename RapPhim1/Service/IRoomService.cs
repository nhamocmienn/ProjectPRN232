using RapPhim1.DTO.Admin.room;
using RapPhim1.Models;

namespace RapPhim1.Service
{
    public interface IRoomService
    {
        Task<List<RoomDTO>> GetAllAsync();
        Task<Room?> GetByIdAsync(int id);
        Task AddAsync(Room room);
        Task UpdateAsync(Room room);
        Task DeleteAsync(int id);
    }
}
