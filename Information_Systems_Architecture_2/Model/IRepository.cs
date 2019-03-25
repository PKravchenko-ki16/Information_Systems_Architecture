using System.Collections.Generic;

namespace Model
{
    public interface IRepository<T>
        where T : DomainObject, new()
    {
        IEnumerable<T> GetAll();
        void Delete(T obj);
        void Add(T obj);
    }
}
