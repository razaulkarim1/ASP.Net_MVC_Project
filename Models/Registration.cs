using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_01.Models
{
    public class Registration
    {
        public int Id { get; set; }
        [Required, StringLength(100), Display(Name ="User Name")]
        public string UserName { get; set; }
        [Required, StringLength(10), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, StringLength(10), Compare("Password"), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}