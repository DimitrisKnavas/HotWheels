using HotWheels.Models;
using HotWheels.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotWheels.Service
{
    public class CarService : ICarService,IDisposable
    {
        private ICarRepository _carRepo;

        public CarService(ICarRepository carRepo)
        {
            _carRepo = carRepo;
        }

        public IEnumerable<CarViewModel> GetAllCars()
        {
            return _carRepo.GetAll();
        }

        public IEnumerable<CarViewModel> GetCarsBySearchCriteria(string criteria)
        {
            return _carRepo.GetAll().Where(x=> x.Brand.Contains(criteria) || x.Model.Contains(criteria));
        }

        public IEnumerable<CarViewModel> GetCarsByCreatedDate()
        {
            return _carRepo.GetAll().OrderByDescending(c => c.Created).Take(4);
        }

        public IEnumerable<CarViewModel> GetCarsByViews()
        {
            return _carRepo.GetAll().OrderByDescending(c => c.Views).Take(4);
        }

        public CarViewModel CarDetails(int? id)
        {
            return _carRepo.GetById(id).FirstOrDefault();
        }

        public void CreateCar(CarViewModel car)
        {
            _carRepo.Insert(car);
            _carRepo.Save();
        }

        public void EditCar(CarViewModel car)
        {
            _carRepo.Update(car);
            _carRepo.Save();
        }

        public void Delete(int? id)
        {
            _carRepo.Delete(id);
            _carRepo.Save();
        }

        public void Dispose()
        {
            _carRepo.Dispose();
        }
    }
}