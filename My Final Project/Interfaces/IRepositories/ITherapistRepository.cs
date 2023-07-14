using My_Final_Project.Models.Entities;
using System.Linq.Expressions;

namespace My_Final_Project.Interfaces.IRepositories
{
    public interface ITherapistRepository : IBaseRepository<Therapist>
    {
        Task<Therapist> GetTherapist(Guid id);
        Task<Therapist> GetTherapist (Expression<Func<Therapist, bool>> expression);
        Task<IEnumerable<Therapist>> GetAllTherapist();
    }
}
