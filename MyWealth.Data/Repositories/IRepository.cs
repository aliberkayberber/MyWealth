using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity); // for db Add operations 

        void Delete(TEntity entity,bool softDelete = true); // for db soft delete and hard delete operations
        
        void Delete(int id); // for db delete operations use id param

        void Update(TEntity entity); // for db update operations

        TEntity GetById(int id); // 

        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetAll(Expression<Func<TEntity,bool >> predicate = null);

        List<TEntity> GetAllList();

        List<TEntity> GetAllEntity(Expression<Func<TEntity, bool>> predicate);

    }
}
