using Microsoft.EntityFrameworkCore;
using My_Final_Project.ApplicationContext;
using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Models.Entities;
using My_Final_Project.Models.Enum;
using System.Linq.Expressions;

namespace My_Final_Project.Implementations.Repositories
{
    public class TherapistRepository : BaseRepository<Therapist>, ITherapistRepository
    {

        public TherapistRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Therapist>> GetAllTherapist()
        {
            return await _context.Therapists.Include(a => a.User).ToListAsync();
        }

        public async Task<IEnumerable<Therapist>> GetApprovedTherapist()
        {
            return await _context.Therapists.Where(x => x.Status == Aprrove.Approved).ToListAsync();
        }

        public async Task<Therapist> GetTherapist(Guid id)
        {
            return await _context.Therapists.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Therapist> GetTherapist(Expression<Func<Therapist, bool>> expression)
        {
            return await _context.Therapists.Include(a => a.User).FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Therapist>> GetUnapprovedTherapist()
        {
            return await _context.Therapists.Where(x => x.Status == Aprrove.Pending).ToListAsync();
        }
    }
}
