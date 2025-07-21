using RapPhim1.DAO;
using RapPhim1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RapPhim1.Service
{
    public class SeatTypeService : ISeatTypeService
    {
        private readonly SeatTypeDAO _seatTypeDAO;

        public SeatTypeService(SeatTypeDAO seatTypeDAO) => _seatTypeDAO = seatTypeDAO;

        public async Task<List<SeatType>> GetAllAsync()
        {
            return await _seatTypeDAO.GetAllAsync();
        }

        public async Task<SeatType?> GetByIdAsync(int id)
        {
            return await _seatTypeDAO.GetByIdAsync(id);
        }

        public async Task AddAsync(SeatType seatType)
        {
            await _seatTypeDAO.AddAsync(seatType);
            await _seatTypeDAO.SaveChangesAsync();
        }

        public async Task UpdateAsync(SeatType seatType)
        {
            _seatTypeDAO.Update(seatType);
            await _seatTypeDAO.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var seatType = await _seatTypeDAO.GetByIdAsync(id);
            if (seatType != null)
            {
                _seatTypeDAO.Delete(seatType);
                await _seatTypeDAO.SaveChangesAsync();
            }
        }
    }
}