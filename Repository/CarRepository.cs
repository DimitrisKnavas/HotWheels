using HotWheels.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace HotWheels.Repository
{
    public class CarRepository : IDisposable, ICarRepository
    {
        private HotWheelsContext _context = new HotWheelsContext();

        public IEnumerable<CarViewModel> GetAll()
        {
            var cars = (from ep in _context.Cars
                        join e in _context.Models on ep.ModelId equals e.Id
                        join t in _context.Brands on e.BrandId equals t.Id
                        select new CarViewModel
                        {
                            Id = ep.Id,
                            Brand = t.Name,
                            Model = e.Name,
                            Color = ep.Color,
                            cc = ep.cc,
                            Price = ep.Price,
                            IsNegotiable = ep.IsNegotiable,
                            Description = ep.Description,
                            Created = ep.Created,
                            Views = ep.Views,
                            ImageUrl = ep.ImageUrl
                        }).ToList();

            return cars;
        }

        public void Insert(CarViewModel car)
        {
            var lastRecord = _context.Cars.OrderByDescending(x => x.Id).FirstOrDefault();
            IQueryable<Model> carModelId = _context.Models.Where(x => x.Name == car.Model);

            Car carTOadd = new Car() { Id = lastRecord.Id + 1, Color = car.Color, ModelId = carModelId.FirstOrDefault().Id, Model = carModelId.FirstOrDefault() , cc = car.cc , Price = car.Price, IsNegotiable = car.IsNegotiable, Description = car.Description, Created = DateTime.UtcNow , ImageUrl = car.ImageUrl};
            
            _context.Cars.Add(carTOadd);
            
        }

        public void Update(CarViewModel car)
        {
            //not necessery because from modelId WE KNOW the brand
            //brand doesn't need to be updated
            //var carBrandId = _context.Brands.Where(x => x.Name == car.Brand);
            IQueryable<Model> carModelId = _context.Models.Where(x => x.Name == car.Model);

            Car carTOupdate = new Car() { Id = car.Id, Color = car.Color, ModelId = carModelId.FirstOrDefault().Id, Model = carModelId.FirstOrDefault(), cc = car.cc, Price = car.Price, IsNegotiable = car.IsNegotiable, Description = car.Description, Created = car.Created,Views = car.Views, ImageUrl = car.ImageUrl};


            _context.Entry(carTOupdate).State = EntityState.Modified;
            
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IQueryable<CarViewModel> GetById(int? id)
        {
            var car = (from ep in _context.Cars
                       join e in _context.Models on ep.ModelId equals e.Id
                       join t in _context.Brands on e.BrandId equals t.Id
                       where ep.Id == id
                       select new CarViewModel
                       {
                           Id = ep.Id,
                           Brand = t.Name,
                           Model = e.Name,
                           Color = ep.Color,
                           cc = ep.cc,
                           Price = ep.Price,
                           IsNegotiable = ep.IsNegotiable,
                           Description = ep.Description,
                           Created = ep.Created,
                           Views = ep.Views,
                           ImageUrl = ep.ImageUrl
                       });

            return car;
        }

        public void Delete(int? id)
        {
            var car = _context.Cars.Find(id);
            _context.Cars.Remove(car);
            
        }
    }
}