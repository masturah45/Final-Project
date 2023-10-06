using My_Final_Project.Models.Entities;
using System.Linq.Expressions;

namespace My_Final_Project.Interfaces.IRepositories
{
    public interface ITherapistIssuesRepository : IBaseRepository
    {
        Task<TherapistIssue> GetTherapistIssues(Guid id);
        Task<TherapistIssue> GetTherapistIssues(Expression<Func<TherapistIssue, bool>> expression);
        Task<IEnumerable<TherapistIssue>> GetAllTherapistIssues();
        Task<IEnumerable<TherapistIssue>> GetTherapistByIssues(Guid Issuesid);
    }
}
