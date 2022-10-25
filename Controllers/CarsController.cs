using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using work_01.Models;
using work_01.Models.ViewModel;
namespace work_01.Controllers
{
    [Authorize]
    public class CarsController : Controller
    {
        CarRentDbContext db = new CarRentDbContext();
        // GET: Cars
        public ActionResult Index()
        {
            ViewBag.types = new SelectList(db.Types, "Id", "CarType");
            return View(db.Cars.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.types = new SelectList(db.Types, "Id", "CarType");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarVM vm)
        {
            using (var context = new CarRentDbContext())
            {
                using (DbContextTransaction dbTran = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            if (vm.Pictures != null)
                            {
                                string filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(vm.Pictures.FileName));
                                vm.Pictures.SaveAs(Server.MapPath(filePath));

                                Car c = new Car
                                {
                                    CarName = vm.CarName,
                                    TypeId = vm.TypeId,
                                    Make = vm.Make,
                                    RecordDate = vm.RecordDate,
                                    PicturePath = filePath,
                                    isAvailable = vm.isAvailable
                                };
                                db.Cars.Add(c);
                                db.SaveChanges();
                                ViewBag.msg = "Data inserted Successfully";
                                return PartialView("_success");
                            }
                        }
                        dbTran.Commit();
                    }
                    catch (Exception)
                    {
                        dbTran.Rollback();
                        ViewBag.msg = "Data insertion Failed for one or more missing";
                        return PartialView("_error");
                    }
                }
            }
            ViewBag.types = new SelectList(db.Types, "Id", "CarType");
            return View();
        }

        public ActionResult Edit(int? id) 
        {
            Car c = db.Cars.Find(id);
            CarVM vm = new CarVM 
            {
                CarId=c.CarId,
                CarName=c.CarName,
                TypeId=c.TypeId,
                Make=c.Make,
                RecordDate=c.RecordDate,
                PicturePath=c.PicturePath,
                isAvailable=c.isAvailable
            };
            ViewBag.types = new SelectList(db.Types, "Id", "CarType");
            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(CarVM cvm)
        
        {
            if (ModelState.IsValid)
            {
                string filePath = cvm.PicturePath;
                if (cvm.Pictures != null)
                {
                    filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(cvm.Pictures.FileName));
                    cvm.Pictures.SaveAs(Server.MapPath(filePath));

                    Car c = new Car
                    {
                        CarId = cvm.CarId,
                        CarName = cvm.CarName,
                        TypeId = cvm.TypeId,
                        Make = cvm.Make,
                        RecordDate = cvm.RecordDate,
                        PicturePath = filePath,
                        isAvailable = cvm.isAvailable
                    };
                    db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return PartialView("_success");
                }
                else
                {
                    Car c = new Car
                    {
                        CarId = cvm.CarId,
                        CarName = cvm.CarName,
                        TypeId = cvm.TypeId,
                        Make=cvm.Make,
                        RecordDate = cvm.RecordDate,
                        isAvailable = cvm.isAvailable
                    };
                    db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return PartialView("_success");
                }
            }
            ViewBag.types = new SelectList(db.Types, "Id", "CarType");
            return PartialView("_error");
        }

        public ActionResult Delete(int? id)
        {
            if (id != null) 
            {
                Car c = db.Cars.Find(id);
                db.Cars.Remove(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        

        //public ActionResult Report()
        //{
        //    var cars = db.Cars.ToList();
        //    return View(cars);
        //}

        //public ActionResult ExportCar() 
        //{
        //    List<Car> carList = new List<Car>();
        //    carList = db.Cars.ToList();

        //    ReportDocument rd = new ReportDocument();
        //    rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), "rptCarReports.rpt"));
        //    rd.SetDataSource(carList);

        //    Response.Buffer = false;
        //    Response.ClearContent();
        //    Response.ClearHeaders();

        //    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //    stream.Seek(0, SeekOrigin.Begin);
        //    return File(stream, "application/pdf", "CarList.pdf");
        //}
    }
}