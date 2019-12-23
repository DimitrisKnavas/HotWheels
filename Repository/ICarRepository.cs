using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotWheels.Models;

namespace HotWheels.Repository
{
    public interface ICarRepository
    {
        void Delete(int? id);
        void Dispose();
        IEnumerable<CarViewModel> GetAll();
        IQueryable<CarViewModel> GetById(int? id);
        void Insert(CarViewModel car);
        void Save();
        void Update(CarViewModel car);
    }
}