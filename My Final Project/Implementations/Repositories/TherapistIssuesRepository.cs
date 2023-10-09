using Microsoft.EntityFrameworkCore;
using My_Final_Project.ApplicationContext;
using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Models.Entities;
using System.Linq.Expressions;

namespace My_Final_Project.Implementations.Repositories
{
    public class TherapistIssuesRepository : BaseRepository, ITherapistIssuesRepository
    {
        public TherapistIssuesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TherapistIssue>> GetAllTherapistIssues()
        {
            return await _context.TherapistIssues.Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<TherapistIssue>> GetTherapistByIssues(Guid Issuesid)
        {
            return await _context.TherapistIssues.Where(a => a.IssueId == Issuesid).Include(a => a.Therapist).ToListAsync();
        }

        public async Task<TherapistIssue> GetTherapistIssues(Guid id)
        {
            return await _context.TherapistIssues.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<TherapistIssue> GetTherapistIssues(Expression<Func<TherapistIssue, bool>> expression)
        {
            return await _context.TherapistIssues.SingleOrDefaultAsync(expression);
        }
    }
}
