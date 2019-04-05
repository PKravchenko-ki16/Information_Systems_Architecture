using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoDEL
{
    internal abstract class AdoRepository<T> : IRepository<T> where T : IDomainObject, new()
    {
        private readonly List<T> _added = new List<T>();
        protected List<T> Added { get { return _added; } }

        private readonly List<T> _deleted = new List<T>();
        protected List<T> Deleted { get { return _deleted; } }

        public abstract IEnumerable<T> GetAll();
        public abstract string GetUpdateScript();

        public void Delete(T obj)
        {
            if (_added.Contains(obj))
                _added.Remove(obj);
            else if (!_deleted.Contains(obj))
                _deleted.Add(obj);
        }

        public void Add(T obj)
        {
            if (_deleted.Contains(obj))
                _deleted.Remove(obj);
            if (!_added.Contains(obj))
                _added.Add(obj);
        }

        protected IEnumerable<int> DeletedIds
        {
            get { return Deleted.Select(o => o.Id).Distinct(); }
        }

        public void Discard()
        {
            Added.Clear();
            Deleted.Clear();
        }

        public T Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(T obj)
        {
            throw new NotImplementedException();
        }

        public void Create(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
