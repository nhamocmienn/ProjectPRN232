using RapPhim1.DAO;
using RapPhim1.DTO.Ticket;
using RapPhim1.Models;

namespace RapPhim1.Service
{
    public class OrderService : IOrderService
    {
        private readonly OrderDAO _dao;

        public OrderService(OrderDAO dao)
        {
            _dao = dao;
        }

        public async Task<Guid> CreateOrderAsync(CreateOrderDTO dto)
        {
            bool isOffline = dto.UserId == null;

            // ✅ Check trùng ghế trước khi tạo
            foreach (var ticket in dto.Tickets)
            {
                bool taken = await _dao.IsSeatTakenAsync(ticket.ShowtimeId, ticket.SeatId);
                if (taken)
                    throw new Exception($"Ghế {ticket.SeatId} trong suất chiếu {ticket.ShowtimeId} đã được đặt.");
            }

            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                OrderDate = DateTime.Now,
                Status = isOffline ? "Paid" : "Pending",
                TotalAmount = dto.Tickets.Sum(t => t.Price) + dto.Services.Sum(s => s.UnitPrice * s.Quantity),
                OrderTickets = dto.Tickets.Select(t => new OrderTicket
                {
                    Id = Guid.NewGuid(),
                    ShowtimeId = t.ShowtimeId,
                    SeatId = t.SeatId,
                    Price = t.Price
                }).ToList(),
                OrderServices = dto.Services.Select(s => new RapPhim1.Models.OrderService
                {
                    ServiceItemId = s.ServiceItemId,
                    Quantity = s.Quantity,
                    UnitPrice = s.UnitPrice
                }).ToList()
            };

            return await _dao.CreateOrderAsync(order);
        }


        public async Task<bool> MarkOrderAsPaidAsync(Guid orderId)
        {
            var order = await _dao.GetOrderByIdAsync(orderId);
            if (order == null || order.Status != "Pending") return false;

            order.Status = "Paid";
            await _dao.SaveChangesAsync(); //  Chỉ lưu thay đổi
            return true;
        }


        public async Task<bool> DeleteIfExpiredAsync(Guid orderId)
        {
            var order = await _dao.GetOrderByIdAsync(orderId);
            if (order == null || order.Status != "Pending") return false;

            if (DateTime.Now > order.OrderDate.AddMinutes(15))
            {
                await _dao.DeleteOrderAsync(order);
                return true;
            }

            return false;
        }

        public Task<List<RevenueReportDTO>> GetRevenueAsync(string type)
        {
            return _dao.GetRevenueAsync(type);
        }


    }

    public interface IOrderService
    {
        Task<Guid> CreateOrderAsync(CreateOrderDTO dto);
        Task<bool> MarkOrderAsPaidAsync(Guid orderId);
        Task<bool> DeleteIfExpiredAsync(Guid orderId);
        Task<List<RevenueReportDTO>> GetRevenueAsync(string type);
    }


}
