using System.Collections.Generic;
using Model;

namespace EntityDAL
{
    public class EntityRepository<T> : IRepository<T> where T : class, IDomainObject, new()
    {
        private readonly DataContext _context;

        public EntityRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }
        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Delete(T obj)
        {
            _context.Set<T>().Remove(obj);
        }

        public void Create(T obj)
        {
            _context.Set<T>().Add(obj);
        }

        public void Update(T obj)
        {
            //_context.Set<T>();
        }
    }
}