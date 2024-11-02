using Microsoft.EntityFrameworkCore;
using MyWealth.Data.Context;
using MyWealth.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly MyWealthDbContext _db; 
        private readonly DbSet<TEntity> _dbSet;

        public Repository(MyWealthDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
        }

        // for db Add operations 
        public void Add(TEntity entity)
        {
           _dbSet.Add(entity);
        }

        // for db soft delete and hard delete operations
        public void Delete(TEntity entity, bool softDelete = true)
        {
            if (softDelete) 
            {
                entity.ModifiedDate = DateTime.Now;
                entity.IsDeleted = true;
                _dbSet.Update(entity);
            }
            else
            {
                _dbSet.Remove(entity); // hard delete
            }
        }

        // for db delete operations use id param
        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }

        // checking entity by linq Expression
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        // checking entitys by linq Expression or checking all entity
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate is null ? _dbSet : _dbSet.Where(predicate);
        }

        // checking entity by id
        public TEntity GetById(int id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);
        }

        // for db update operations
        public void Update(TEntity entity)
        {
            entity.ModifiedDate= DateTime.Now;
            _dbSet.Update(entity);
        }
        /*
        public List<TEntity> GetAllEntity(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public List<TEntity> GetAllList()
        {
            return _dbSet.ToList();
        }
        */
    }
}
