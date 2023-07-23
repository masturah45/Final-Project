using Microsoft.EntityFrameworkCore;
using My_Final_Project.ApplicationContext;
using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Models.Entities;
using System.Linq.Expressions;

namespace My_Final_Project.Implementations.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users
                .Include(b => b.UserRoles)
                .ThenInclude(c => c.Role)
                .ToListAsync();
        }

        public async Task<User> GetUser(Guid id)
        {
            return await _context.Users
                .Include(c => c.UserRoles)
                .ThenInclude(c => c.Role)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> Get(Expression<Func<User, bool>> expression)
        {
            var user = _context.Users
                .Include(a => a.SuperAdmin)
                .Include(b => b.Therapist)
                .Include(c => c.Client)
                .Include(d => d.UserRoles)
                .ThenInclude(e => e.Role)
                .FirstOrDefaultAsync(expression);
            return await user;
        }
    }
}
