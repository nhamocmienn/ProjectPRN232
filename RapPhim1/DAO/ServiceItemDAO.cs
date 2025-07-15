using RapPhim1.Data;
using RapPhim1.Models;
using Microsoft.EntityFrameworkCore;

namespace RapPhim1.DAO
{
    public class ServiceItemDAO
    {
        private readonly AppDbContext _context;
        public ServiceItemDAO(AppDbContext context) => _context = context;

        public async Task<List<ServiceItem>> GetAllAsync() =>
            await _context.ServiceItems.ToListAsync();

        public async Task<ServiceItem?> GetByIdAsync(int id) =>
            await _context.ServiceItems.FindAsync(id);

        public async Task AddAsync(ServiceItem item) =>
            await _context.ServiceItems.AddAsync(item);

        public void Update(ServiceItem item) =>
            _context.ServiceItems.Update(item);

        public void Delete(ServiceItem item) =>
            item.IsActive = false;

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
