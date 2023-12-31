﻿using Microsoft.EntityFrameworkCore;
using My_Final_Project.ApplicationContext;
using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Models.Entities;
using System.Linq.Expressions;

namespace My_Final_Project.Implementations.Repositories
{
    public class BaseRepository: IBaseRepository
    {
        protected ApplicationDbContext _context;
        
        public async Task<T> Add<T>(T entity) where T : BaseEntity
        {
           await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();  
            return entity;
        }

        public async void Delete<T>(T entity) where T : BaseEntity
        {
             _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public async Task<T> Get<T>(Guid id) where T : BaseEntity
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id); 
        }

        public async Task<T> Get<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : BaseEntity
        {
            return await _context.Set<T>().ToListAsync();
        }

        public IQueryable<T> QueryWhere<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
        {
            return _context.Set<T>().Where(expression).ToList().AsQueryable();
        }

        public async Task<bool> save()
        {
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<T> Update<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
