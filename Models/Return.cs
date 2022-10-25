using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace work_01.Models
{
    public class Return
    {
        public int Id { get; set; }
        [ForeignKey("Car"), Column(Order = 1)]
        public int CarId { get; set; }
        [ForeignKey("Customer"), Column(Order = 2)]
        public int CustomerId { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Return Date"), DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReturnDate { get; set; }
        public decimal? Fine { get; set; }
        public virtual Car Car { get; set; }
        public virtual Customer Customer { get; set; }
    }
}