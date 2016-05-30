using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AirPortWebApi.Infrastructure.Repositories
{
    public interface IRepository<T> :IDisposable where T :class
    {
        void Add(T entity);
        void Delete(T entity);
        void Delete(object id);
        void Update(T entity);

        void SaveChanges([CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);

        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);

        // remove all connectings from result query 
        IEnumerable<T> GetReadOnly(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);


        void DetachAddedEntities();
        void Delete(IEnumerable<T> entitiesToDelete);
    }
}
