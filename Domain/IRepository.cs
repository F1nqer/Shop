using System.Linq;

namespace Domain
{
    public interface IRepository<T> where T : class
    {
        public IQueryable<T> GetAll();
        T GetById(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int Id);
    }
}
