using System.Linq;

namespace Locatarium.DAL.IDAO
{
    public interface IAbstractDAO<T>
    {
        IQueryable<T> GetAll();

        void SaveChanges();

        void Add(T entity);

        T Update(T entity);

        void Delete(T entityToDelete);
    }
}