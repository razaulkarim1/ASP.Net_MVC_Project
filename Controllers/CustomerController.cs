using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using work_01.Models;

namespace work_01.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        CarRentDbContext db = new CarRentDbContext();
        // GET: Customer
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        public ViewResult Create() 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer c) 
        {
            if (ModelState.IsValid) 
            {
                db.Customers.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c);
        }

        public PartialViewResult Edit(int? id) 
        {
            Customer c = db.Customers.Find(id);
            return PartialView("_EditCustomer",c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer c) 
        {
            if (ModelState.IsValid) 
            {
                db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int? id) 
        {
            if (id != null) 
            {
                Customer c = db.Customers.Find(id);
                db.Customers.Remove(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}