using RapPhim1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RapPhim1.Service
{
    public interface ISeatTypeService
    {
        Task<List<SeatType>> GetAllAsync();
        Task<SeatType?> GetByIdAsync(int id);
        Task AddAsync(SeatType seatType);
        Task UpdateAsync(SeatType seatType);
        Task DeleteAsync(int id);
    }
}