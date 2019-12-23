using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotWheels.Models;
using HotWheels.Repository;
using HotWheels.Service;
using Newtonsoft.Json;

namespace HotWheels.Controllers
{
    public class CarsController : Controller
    {
        private ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        // GET: Cars
        public ActionResult Index()
        {
            IEnumerable<CarViewModel> cars = _carService.GetAllCars();
            return View(cars);
        }

        
        public ActionResult Search(string id)
        {
            IEnumerable<CarViewModel> cars = _carService.GetCarsBySearchCriteria(id);
            ViewBag.CarName = id;
            return View(cars);
        }

        public JsonResult SearchResult(string id)
        {
            IEnumerable<CarViewModel> cars = _carService.GetCarsBySearchCriteria(id);
            if (cars == null)
            {
                return Json("FAILED", JsonRequestBehavior.AllowGet);
            }
            return Json(cars, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCarById(int id)
        {
            CarViewModel car = _carService.CarDetails(id);
            if (car == null)
            {
                return Json("FAILED", JsonRequestBehavior.AllowGet);
            }
            car.Views++;
            _carService.EditCar(car);
            return Json(car, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLatestCars()
        {
            IEnumerable<CarViewModel> cars = _carService.GetCarsByCreatedDate();
            if (cars == null)
            {
                return Json("FAILED", JsonRequestBehavior.AllowGet);
            }
            return Json(cars, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPopularCars()
        {
            IEnumerable<CarViewModel> cars = _carService.GetCarsByViews();
            if (cars == null)
            {
                return Json("FAILED", JsonRequestBehavior.AllowGet);
            }
            return Json(cars, JsonRequestBehavior.AllowGet);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind(Include = "Brand,Model,Color,cc,Price,IsNegotiable,Description,Image")] CarViewModel car)
        {
            bool success = false;
            if (ModelState.IsValid)
            {
                if (car.Image != null)
                {
                    //Save image to file
                    var extension = Path.GetExtension(car.Image.FileName);
                    var filename = Path.GetFileNameWithoutExtension(car.Image.FileName) + DateTime.UtcNow.ToString("yymmssff") + extension;
                    var filePathOriginal = Server.MapPath("/Images");
                    string savedFileName = Path.Combine(filePathOriginal, filename);
                    car.Image.SaveAs(savedFileName);
                    car.ImageUrl = filename;
                }

                _carService.CreateCar(car);
                success = true;
            }

            return Json(success, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return RedirectToAction("Index");
            }
            CarViewModel car = _carService.CarDetails(id);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult Edit([Bind(Include = "Id,Brand,Model,Color,cc,Price,IsNegotiable,Description")]CarViewModel car)
        {
            CarViewModel carToChange = car;
            bool success = false;
            if (ModelState.IsValid)
            {
                _carService.EditCar(carToChange);
                success = true;
            }

            return Json(success, JsonRequestBehavior.AllowGet);
        }

        //GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarViewModel car = _carService.CarDetails(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Cars/Delete/5

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarViewModel car = _carService.CarDetails(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        //TODO: DeleteCar
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _carService.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _carService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
