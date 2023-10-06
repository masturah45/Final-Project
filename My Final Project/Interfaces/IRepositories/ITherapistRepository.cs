using My_Final_Project.Models.Entities;
using System.Linq.Expressions;

namespace My_Final_Project.Interfaces.IRepositories
{
    public interface ITherapistRepository : IBaseRepository
    {
        Task<Therapist> GetTherapist(Guid id);
        Task<Therapist> CheckIfExist(string email);
        Task<Therapist> GetTherapist (Expression<Func<Therapist, bool>> expression);
        Task<IEnumerable<Therapist>> GetAllTherapist();
        Task<IEnumerable<Therapist>> GetUnapprovedTherapist();
        Task<IEnumerable<Therapist>> GetApprovedTherapist();
        Task<IEnumerable<Therapist>> GetRejectedTherapist();
        Task<IEnumerable<Therapist>> GetAllAvailableTherapist();
        Task<List<Therapist>> GetAllTherapistByChat();
    }
}
