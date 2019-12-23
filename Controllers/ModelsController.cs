using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotWheels.Models;

namespace HotWheels.Controllers
{
    public class ModelsController : Controller
    {
        private HotWheelsContext db = new HotWheelsContext();

        // GET: Models
        public ActionResult Index()
        {
            var models = db.Models.Include(m => m.Brand);
            return View(models.ToList());
        }

        public JsonResult GetModels()
        {
            List<ModelViewModel> models = (from m in db.Models
                                           join b in db.Brands on m.BrandId equals b.Id
                                           select new ModelViewModel { Id = m.Id, Name = m.Name, BrandName = b.Name }).ToList();

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetModelsFromBrandName(string brand)
        {
            List<ModelViewModel> models = (from m in db.Models
                                 join b in db.Brands on m.BrandId equals b.Id
                                 select new ModelViewModel { Id = m.Id , Name = m.Name, BrandName = b.Name}).Where(x=> x.BrandName == brand).ToList();

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        //// GET: Models/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Model model = db.Models.Find(id);
        //    if (model == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(model);
        //}

        // GET: Models/Create
        public ActionResult Create(int id)
        {
            //ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name");
            ViewBag.BrandId = id;
            return View();
        }

        // POST: Models/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,BrandId")] Model model)
        {
            if (ModelState.IsValid)
            {
                db.Models.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index","Cars");
            }

            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", model.BrandId);
            return View(model);
        }

        //// GET: Models/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Model model = db.Models.Find(id);
        //    if (model == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", model.BrandId);
        //    return View(model);
        //}

        //// POST: Models/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,BrandId")] Model model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(model).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", model.BrandId);
        //    return View(model);
        //}

        // GET: Models/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Model model = db.Models.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Models/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Model model = db.Models.Find(id);
            db.Models.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index","Cars");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
