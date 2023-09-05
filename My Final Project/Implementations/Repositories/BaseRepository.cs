using Microsoft.EntityFrameworkCore;
using My_Final_Project.ApplicationContext;
using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Models.Entities;
using System.Linq.Expressions;

namespace My_Final_Project.Implementations.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        protected ApplicationDbContext _context;
        
        public async Task<T> Add(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async void Delete(T entity)
        {
             _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public async Task<T> Get(Guid id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id); 
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public IQueryable<T> QueryWhere(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).ToList().AsQueryable();
        }

        public async Task<bool> save()
        {
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<T> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
