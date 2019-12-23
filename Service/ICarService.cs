using System.Collections.Generic;
using System.Web;
using HotWheels.Models;

namespace HotWheels.Service
{
    public interface ICarService
    {
        CarViewModel CarDetails(int? id);
        void CreateCar(CarViewModel car);
        void Delete(int? id);
        void Dispose();
        void EditCar(CarViewModel car);
        IEnumerable<CarViewModel> GetAllCars();
        IEnumerable<CarViewModel> GetCarsBySearchCriteria(string criteria);
        IEnumerable<CarViewModel> GetCarsByCreatedDate();
        IEnumerable<CarViewModel> GetCarsByViews();
    }
}