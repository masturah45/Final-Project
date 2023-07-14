using My_Final_Project.Models.Entities;
using System.Linq.Expressions;

namespace My_Final_Project.Interfaces.IRepositories
{
    public interface ISuperAdminRepository : IBaseRepository<SuperAdmin>
    {
        Task<SuperAdmin> GetSuperAdmin(Guid id);
        Task<SuperAdmin> GetSuperAdmin(Expression<Func<SuperAdmin, bool>> expression);
        Task<IEnumerable<SuperAdmin>> GetAllSuperAdmins();
    }
}
