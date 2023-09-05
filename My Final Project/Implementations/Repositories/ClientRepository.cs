using Microsoft.EntityFrameworkCore;
using My_Final_Project.ApplicationContext;
using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Models.Entities;
using System.Linq.Expressions;

namespace My_Final_Project.Implementations.Repositories
{
    public class ClientRepository : BaseRepository<Client> , IClientRepository
    {
        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Client> CheckIfExist(string email)
        {
            return await _context.Clients.Where(e => e.User.Email.Equals(email)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _context.Clients.Include(a => a.User).Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<List<Client>> GetAllClientByChat()
        {
            return await _context.Clients.Include(a => a.User).ToListAsync();
        }

        public async Task<Client> GetClient(Guid id)
        {
            return await _context.Clients.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == id);
        }
         public async Task<Client> GetClientByIdAsync(Guid id)
        {
            return await _context.Clients.Include(a => a.User).FirstOrDefaultAsync(a => a.UserId == id);
        }

        public async Task<Client> GetClient(Expression<Func<Client, bool>> expression)
        {
            return await _context.Clients.Include(a => a.User).FirstOrDefaultAsync(expression);
        }
    }
}
