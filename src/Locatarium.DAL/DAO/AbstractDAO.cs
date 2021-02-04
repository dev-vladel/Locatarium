using Locatarium.DAL.IDAO;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Locatarium.DAL.DAO
{
    public class AbstractDAO<T> : IAbstractDAO<T> where T : class
    {
        protected DbSet<T> _dbSet;

        protected DbContext _dbContext;

        public AbstractDAO(DbContext dbContext)
        {
            _dbContext = dbContext;

            _dbSet = dbContext.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual T Update(T entity)
        {
            return _dbSet.Update(entity).Entity;
        }

        public virtual void Delete(T entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
        }
    }
}