using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace work_01.Models
{
    public class Customer
    {
        public Customer()
        {
            this.Rent = new List<Rent>();
            this.Returns = new List<Return>();
        }
        public int CustomerId { get; set; }
        [Required, StringLength(50, ErrorMessage = "The field is required!!"), Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Required, StringLength(100, ErrorMessage = "The field is required!!")]
        public string Address { get; set; }
        [Required, StringLength(14, ErrorMessage = "The field is required!!")]
        public string Mobile { get; set; }
        [Required, StringLength(40, ErrorMessage = "The field is required!!")]
        public string Email { get; set; }
        public virtual ICollection<Rent> Rent { get; set; }
        public virtual ICollection<Return> Returns { get; set; }
    }
}