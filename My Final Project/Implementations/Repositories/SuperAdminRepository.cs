using Microsoft.EntityFrameworkCore;
using My_Final_Project.ApplicationContext;
using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Models.Entities;
using System.Linq.Expressions;

namespace My_Final_Project.Implementations.Repositories
{
    public class SuperAdminRepository : BaseRepository<SuperAdmin> , ISuperAdminRepository
    {
        public SuperAdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SuperAdmin>> GetAllSuperAdmins()
        {
            return await _context.SuperAdmins.Include(a => a.User).ToListAsync();
        }

        public async Task<SuperAdmin> GetSuperAdmin(Guid id)
        {
            return await _context.SuperAdmins.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<SuperAdmin> GetSuperAdmin(Expression<Func<SuperAdmin, bool>> expression)
        {
            return await _context.SuperAdmins.Include(a => a.User).FirstOrDefaultAsync(expression);
        }




    }
}
