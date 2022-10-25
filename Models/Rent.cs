using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace work_01.Models
{
    public class Rent
    {
        public int Id { get; set; }
        [ForeignKey("Car"), Column(Order = 1), Display(Name = "Car")]
        public int CarId { get; set; }
        [ForeignKey("Customer"), Column(Order = 2), Display(Name = "Customer")]
        public int CustomerId { get; set; }
        [Required, Display(Name = "Rent Fee"), Range(1000, 50000)]
        public double RentFee { get; set; }
        [Required, Display(Name = "Start Date"), DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Required, Display(Name = "End Date"), DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public virtual Car Car { get; set; }
        public virtual Customer Customer { get; set; }
    }
}