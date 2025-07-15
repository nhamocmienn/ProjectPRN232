using RapPhim1.DTO.Ticket;
using RapPhim1.Models;
using RapPhim1.Data;
using Microsoft.EntityFrameworkCore;

namespace RapPhim1.DAO
{
    public class OrderDAO 
    {
        private readonly AppDbContext _context;

        public OrderDAO(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }

        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task DeleteOrderAsync(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<RevenueReportDTO>> GetRevenueAsync(string type)
        {
            if (type == "Movie")
            {
                return await _context.OrderTickets
                    .Include(x => x.Showtime.Movie)
                    .GroupBy(x => x.Showtime.Movie.Title)
                    .Select(g => new RevenueReportDTO
                    {
                        Type = "Movie",
                        Label = g.Key,
                        TotalRevenue = g.Sum(x => x.Price)
                    }).ToListAsync();
            }

            if (type == "Day")
            {
                return await _context.Orders
                    .GroupBy(o => o.OrderDate.Date)
                    .Select(g => new RevenueReportDTO
                    {
                        Type = "Day",
                        Label = g.Key.ToString("yyyy-MM-dd"),
                        TotalRevenue = g.Sum(x => x.TotalAmount)
                    }).ToListAsync();
            }

            if (type == "Month")
            {
                return await _context.Orders
                    .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                    .Select(g => new RevenueReportDTO
                    {
                        Type = "Month",
                        Label = $"{g.Key.Year}-{g.Key.Month:D2}",
                        TotalRevenue = g.Sum(x => x.TotalAmount)
                    }).ToListAsync();
            }

            if (type == "Service")
            {
                return await _context.OrderServices
                    .Include(x => x.ServiceItem)
                    .GroupBy(x => x.ServiceItem.Name)
                    .Select(g => new RevenueReportDTO
                    {
                        Type = "Service",
                        Label = g.Key,
                        TotalRevenue = g.Sum(x => x.UnitPrice * x.Quantity)
                    }).ToListAsync();
            }

            return new();
        }

        public async Task<bool> IsSeatTakenAsync(int showtimeId, int seatId)
        {
            return await _context.OrderTickets.AnyAsync(ot =>
                ot.ShowtimeId == showtimeId &&
                ot.SeatId == seatId &&
                ot.Order.Status != "Failed"); 
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
