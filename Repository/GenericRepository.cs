using HotWheels.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotWheels.Repository
{
    public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
    {

        private HotWheelsContext _context = null;
        private DbSet<T> carTable = null;

        public GenericRepository()
        {
            _context = new HotWheelsContext();
            carTable = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return carTable.ToList();
        }

        public T GetById(int? id)
        {
            return carTable.Find(id);
        }

        public void Insert(T obj)
        {
            carTable.Add(obj);
        }

        public void Update(T obj)
        {
            carTable.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(int? id)
        {
            T recordToDelete = carTable.Find(id);
            carTable.Remove(recordToDelete);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}