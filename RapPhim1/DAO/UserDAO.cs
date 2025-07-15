using Microsoft.EntityFrameworkCore;
using RapPhim1.Data;
using RapPhim1.Models;

namespace RapPhim1.DAO
{
    public class UserDAO
    {
        private readonly AppDbContext _context;

        public UserDAO(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User?> GetByIdAsync(int id) =>
            await _context.Users.FindAsync(id);

        public async Task AddAsync(User user) =>
            await _context.Users.AddAsync(user);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}

