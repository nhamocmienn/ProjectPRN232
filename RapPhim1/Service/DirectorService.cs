using RapPhim1.DTO.Admin.Director;
using RapPhim1.DAO;
using RapPhim1.Models;

namespace RapPhim1.Service
{
    public class DirectorService : IDirectorService
    {
        private readonly DirectorDAO _dao;
        public DirectorService(DirectorDAO dao) => _dao = dao;

        public async Task<List<DirectorDTO>> GetAllAsync()
        {
            var list = await _dao.GetAllAsync();
            return list.Select(d => new DirectorDTO
            {
                Id = d.Id,
                Name = d.Name,
                Biography = d.Biography,
                ImageUrl = d.ImageUrl,
                IsActive = d.IsActive
            }).ToList();
        }

        public async Task<DirectorDTO?> GetByIdAsync(int id)
        {
            var d = await _dao.GetByIdAsync(id);
            if (d == null) return null;

            return new DirectorDTO
            {
                Id = d.Id,
                Name = d.Name,
                Biography = d.Biography,
                ImageUrl = d.ImageUrl,
                IsActive = d.IsActive
            };
        }

        public async Task AddAsync(DirectorCreateUpdateDto dto)
        {
            var director = new Director
            {
                Name = dto.Name,
                Biography = dto.Biography,
                ImageUrl = dto.ImageUrl,
                IsActive = true
            };

            await _dao.AddAsync(director);
            await _dao.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, DirectorCreateUpdateDto dto)
        {
            var director = await _dao.GetByIdAsync(id);
            if (director == null) return;

            director.Name = dto.Name;
            director.Biography = dto.Biography;
            director.ImageUrl = dto.ImageUrl;

            _dao.Update(director);
            await _dao.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var director = await _dao.GetByIdAsync(id);
            if (director == null) return;

            _dao.Delete(director);
            await _dao.SaveChangesAsync();
        }
    }

    public interface IDirectorService
    {
        Task<List<DirectorDTO>> GetAllAsync();
        Task<DirectorDTO?> GetByIdAsync(int id);
        Task AddAsync(DirectorCreateUpdateDto dto);
        Task UpdateAsync(int id, DirectorCreateUpdateDto dto);
        Task DeleteAsync(int id);
    }

}
