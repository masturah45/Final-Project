using My_Final_Project.Models.Entities;
using System.Linq.Expressions;

namespace My_Final_Project.Interfaces.IRepositories
{
    public interface IBaseRepository<T> where T : BaseEntity, new()
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        void Delete(T entity);
        bool save();
        Task<T> Get(Guid id);
        Task<T> Get(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAll();
        IQueryable<T> QueryWhere(Expression<Func<T, bool>> expression);


    }
}
