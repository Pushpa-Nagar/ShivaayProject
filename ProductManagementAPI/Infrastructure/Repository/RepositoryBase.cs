using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductManagementAPI.Infrastructure.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly ShivaayProductDBContext _context;

        public RepositoryBase(ShivaayProductDBContext context)
        {
            _context = context;
        }
        protected void Save() => _context.SaveChanges();

        public void Create(T entity)
        {
            _context.Add(entity);
        }

        public void CreateList(List<T> entity)
        {
            _context.AddRange(entity);
        }
        public T CreateAndReturn(T entity)
        {
            _context.Add(entity);
            return entity;
        }

        public List<T> CreateAndReturnList(List<T> entity)
        {
            _context.AddRange(entity);
            return entity;
        }

        public List<T> UpdateAndReturnList(List<T> entity)
        {
            _context.UpdateRange(entity);
            return entity;
        }

        public virtual void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public T GetById(long id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public T UpdateAndReturn(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }

        public T Single(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).FirstOrDefault();
        }

        public int Count(Expression<Func<T, Boolean>> predicate)
        {
            return _context.Set<T>().Where(predicate).Count();
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Any(predicate);
        }

        public bool Any()
        {
            return _context.Set<T>().Any();
        }
    }
}

