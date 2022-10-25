using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using work_01.Models;
using work_01.Models.ViewModel;

namespace work_01.Controllers
{
    [Authorize]
    public class RentController : Controller
    {
        CarRentDbContext db = new CarRentDbContext();
        // GET: Rent
        public ActionResult Index()
        {
            ViewBag.cars = new SelectList(db.Cars, "CarId", "CarName");
            ViewBag.customers = new SelectList(db.Customers, "CustomerId", "CustomerName");
            return View(db.Rents.ToList());
        }

        public ActionResult Create() 
        {
            ViewBag.cars = new SelectList(db.Cars, "CarId", "CarName");
            ViewBag.customers = new SelectList(db.Customers, "CustomerId", "CustomerName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RentVM rvm) 
        {
            if (ModelState.IsValid) 
            {
                Rent r = new Rent 
                {
                    CarId=rvm.CarId,
                    CustomerId=rvm.CustomerId,
                    StartDate=rvm.StartDate,
                    EndDate=rvm.EndDate,
                    RentFee=rvm.RentFee
                };
                db.Rents.Add(r);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cars = new SelectList(db.Cars, "CarId", "CarName");
            ViewBag.customers = new SelectList(db.Customers, "CustomerId", "CustomerName");
            return View(rvm);
        }

        public ActionResult Edit(int? id) 
        {
            Rent r = db.Rents.Find(id);
            RentVM vm = new RentVM 
            {
                Id=r.Id,
                CarId=r.CarId,
                CustomerId=r.CustomerId,
                StartDate=r.StartDate,
                EndDate=r.EndDate,
                RentFee=r.RentFee
            };
            ViewBag.cars = new SelectList(db.Cars, "CarId", "CarName");
            ViewBag.customers = new SelectList(db.Customers, "CustomerId", "CustomerName");
            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(RentVM vm) 
        {
            if (ModelState.IsValid) 
            {
                Rent r = new Rent 
                {
                    Id=vm.Id,
                    CarId = vm.CarId,
                    CustomerId = vm.CustomerId,
                    StartDate = vm.StartDate,
                    EndDate = vm.EndDate,
                    RentFee = vm.RentFee
                };
                db.Entry(r).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cars = new SelectList(db.Cars, "CarId", "CarName");
            ViewBag.customers = new SelectList(db.Customers, "CustomerId", "CustomerName");
            return View(vm);
        }

        public ActionResult Delete(int? id) 
        {
            if (id != null) 
            {
                Rent r = db.Rents.Find(id);
                db.Rents.Remove(r);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}