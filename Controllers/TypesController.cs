using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using work_01.Models;
using Type = work_01.Models.Type;

namespace work_01.Controllers
{
    [Authorize]
    public class TypesController : Controller
    {
        CarRentDbContext db = new CarRentDbContext();
        // GET: Types
        public ActionResult Index()
        {
            return View(db.Types.ToList());
        }

        public ActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Type type) 
        {
            if (ModelState.IsValid) 
            {
                db.Types.Add(type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(type);
        }
    }
}