using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductManagementAPI.Infrastructure.Repository
{
    public interface IRepositoryBase<T> where T : class
    {
        #region Declaration
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        T Single(Expression<Func<T, bool>> predicate);
        T GetById(long id);
        void Create(T entity);
        void CreateList(List<T> entity);
        T CreateAndReturn(T entity);
        void Update(T entity);
        T UpdateAndReturn(T entity);
        List<T> CreateAndReturnList(List<T> entity);
        List<T> UpdateAndReturnList(List<T> entity);
        void Delete(T entity);
        int Count(Expression<Func<T, bool>> predicate);
        bool Any(Expression<Func<T, bool>> predicate);
        bool Any();

        #endregion
    }
}

