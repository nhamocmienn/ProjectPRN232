using RapPhim1.DAO;
using RapPhim1.DTO.Admin.actor;
using RapPhim1.Models;

namespace RapPhim1.Service
{
    public class ActorService : IActorService
    {
        private readonly ActorDAO _dao;
        public ActorService(ActorDAO dao) => _dao = dao;

        public async Task<List<ActorDTO>> GetAllAsync()
        {
            var list = await _dao.GetAllAsync();
            return list.Select(a => new ActorDTO
            {
                Id = a.Id,
                Name = a.Name,
                Biography = a.Biography,
                ImageUrl = a.ImageUrl,
                IsActive = a.IsActive
            }).ToList();
        }

        public async Task<Actor?> GetEntityByIdAsync(int id) => await _dao.GetByIdAsync(id);

        public async Task<ActorDTO?> GetByIdAsync(int id)
        {
            var a = await _dao.GetByIdAsync(id);
            if (a == null) return null;
            return new ActorDTO
            {
                Id = a.Id,
                Name = a.Name,
                Biography = a.Biography,
                ImageUrl = a.ImageUrl,
                IsActive = a.IsActive
            };
        }

        public async Task AddAsync(ActorCreateUpdateDto dto)
        {
            var a = new Actor
            {
                Name = dto.Name,
                Biography = dto.Biography,
                ImageUrl = dto.ImageUrl,
                IsActive = true
            };
            await _dao.AddAsync(a);
            await _dao.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, ActorCreateUpdateDto dto)
        {
            var a = await _dao.GetByIdAsync(id);
            if (a != null)
            {
                a.Name = dto.Name;
                a.Biography = dto.Biography;
                a.ImageUrl = dto.ImageUrl;
                _dao.Update(a);
                await _dao.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var a = await _dao.GetByIdAsync(id);
            if (a != null)
            {
                _dao.Delete(a);
                await _dao.SaveChangesAsync();
            }
        }
    }

    public interface IActorService
    {
        Task<List<ActorDTO>> GetAllAsync();
        Task<Actor?> GetEntityByIdAsync(int id);
        Task<ActorDTO?> GetByIdAsync(int id);
        Task AddAsync(ActorCreateUpdateDto dto);
        Task UpdateAsync(int id, ActorCreateUpdateDto dto);
        Task DeleteAsync(int id);
    }

}
