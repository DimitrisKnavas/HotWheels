using System.Collections.Generic;

namespace HotWheels.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        void Delete(int? id);
        void Dispose();
        IEnumerable<T> GetAll();
        T GetById(int? id);
        void Insert(T obj);
        void Save();
        void Update(T obj);
    }
}