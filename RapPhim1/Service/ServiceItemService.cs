using RapPhim1.DAO;
using RapPhim1.DTO.Admin.serviceItem;
using RapPhim1.Models;

namespace RapPhim1.Service
{
    public class ServiceItemService : IServiceItemService
    {
        private readonly ServiceItemDAO _dao;
        public ServiceItemService(ServiceItemDAO dao) => _dao = dao;

        public async Task<List<ServiceItemDTO>> GetAllAsync()
        {
            var list = await _dao.GetAllAsync();
            return list.Select(s => new ServiceItemDTO
            {
                Id = s.Id,
                Name = s.Name,
                Price = s.Price,
                ImageUrl = s.ImageUrl,
                IsActive = s.IsActive
            }).ToList();
        }

        public async Task<ServiceItemDTO?> GetByIdAsync(int id)
        {
            var s = await _dao.GetByIdAsync(id);
            if (s == null) return null;

            return new ServiceItemDTO
            {
                Id = s.Id,
                Name = s.Name,
                Price = s.Price,
                ImageUrl = s.ImageUrl,
                IsActive = s.IsActive
            };
        }

        public async Task<ServiceItem?> GetEntityByIdAsync(int id) =>
            await _dao.GetByIdAsync(id);

        public async Task AddAsync(ServiceItem item)
        {
            await _dao.AddAsync(item);
            await _dao.SaveChangesAsync();
        }

        public async Task UpdateAsync(ServiceItem item)
        {
            _dao.Update(item);
            await _dao.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _dao.GetByIdAsync(id);
            if (item != null)
            {
                _dao.Delete(item);
                await _dao.SaveChangesAsync();
            }
        }
    }

    public interface IServiceItemService
    {
        Task<List<ServiceItemDTO>> GetAllAsync();
        Task<ServiceItemDTO?> GetByIdAsync(int id);
        Task<ServiceItem?> GetEntityByIdAsync(int id);
        Task AddAsync(ServiceItem item);
        Task UpdateAsync(ServiceItem item);
        Task DeleteAsync(int id);
    }
}
