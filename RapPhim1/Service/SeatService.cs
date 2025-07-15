using RapPhim1.DAO;
using RapPhim1.DTO.Admin.seat;
using RapPhim1.Models;

namespace RapPhim1.Service
{
    public class SeatService : ISeatService
    {
        private readonly SeatDAO _seatDAO;
        public SeatService(SeatDAO seatDAO) => _seatDAO = seatDAO;

        public async Task<List<SeatDTO>> GetAllAsync()
        {
            var seats = await _seatDAO.GetAllAsync();
            return seats.Select(s => new SeatDTO
            {
                Id = s.Id,
                RoomId = s.RoomId,
                Row = s.Row,
                Column = s.Column,
                SeatTypeId = s.SeatTypeId,
                SeatTypeName = s.SeatType.Name,
                ExtraFee = s.SeatType.ExtraFee
            }).ToList();
        }
        public async Task<Seat?> GetByIdAsync(int id) => await _seatDAO.GetByIdAsync(id);
        public async Task AddAsync(Seat seat)
        {
            await _seatDAO.AddAsync(seat);
            await _seatDAO.SaveChangesAsync();
        }
        public async Task UpdateAsync(Seat seat)
        {
            _seatDAO.Update(seat);
            await _seatDAO.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var seat = await _seatDAO.GetByIdAsync(id);
            if (seat != null)
            {
                _seatDAO.Delete(seat);
                await _seatDAO.SaveChangesAsync();
            }
        }
    }
}
