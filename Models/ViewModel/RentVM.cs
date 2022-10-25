using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace work_01.Models.ViewModel
{
    public class RentVM
    {
        public int Id { get; set; }
        [Display(Name = "Car")]
        public int CarId { get; set; }
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        [Required, Display(Name = "Rent Fee"), Range(1000, 50000)]
        public double RentFee { get; set; }
        [Required, Display(Name = "Start Date"), DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Required, Display(Name = "End Date"), DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
    }
}