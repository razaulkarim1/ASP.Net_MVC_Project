using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace work_01.Models
{
    public class Car
    {
        public Car()
        {
            this.Rent = new List<Rent>();
            this.Returns = new List<Return>();
        }
        public int CarId { get; set; }
        [Required, StringLength(30, ErrorMessage = "The field is required!!"), Display(Name = "Car Name")]
        public string CarName { get; set; }
        [ForeignKey("Type")]
        public int TypeId { get; set; }
        [Required , Display(Name = "Make")]
        public int Make { get; set; }
        [Required, Column(TypeName ="date"),DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true), Display(Name ="Record Date")]
        public DateTime RecordDate { get; set; }
        [Display(Name = "Available")]
        public bool isAvailable { get; set; }
        [Display(Name = "Picture")]
        public string PicturePath { get; set; }
        public virtual Type Type { get; set; }
        public virtual ICollection<Rent> Rent { get; set; }
        public virtual ICollection<Return> Returns { get; set; }
    }

    public class Type
    {
        public Type() 
        {
            this.Cars = new List<Car>();
        }
        public int Id { get; set; }
        [Required ,StringLength(50), Display(Name ="Type")]
        public string CarType { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }

    public class CarRentDbContext : DbContext
    {

        public DbSet<Car> Cars { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Return> Returns { get; set; }
        public DbSet<Registration> Registrations { get; set; }
    }
}