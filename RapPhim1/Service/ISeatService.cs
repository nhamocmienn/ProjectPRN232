using RapPhim1.DTO.Admin.seat;
using RapPhim1.Models;

namespace RapPhim1.Service
{
    public interface ISeatService
    {
        Task<List<SeatDTO>> GetAllAsync();
        Task<Seat?> GetByIdAsync(int id);
        Task AddAsync(Seat seat);
        Task UpdateAsync(Seat seat);
        Task DeleteAsync(int id);
    }
}
