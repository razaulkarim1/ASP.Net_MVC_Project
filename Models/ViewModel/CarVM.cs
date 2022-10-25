using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace work_01.Models.ViewModel
{
    public class CarVM
    {
        public int CarId { get; set; }
        [Required, StringLength(30, ErrorMessage = "The field is required!!"), Display(Name = "Car Name")]
        public string CarName { get; set; }
        [Display(Name ="Type")]
        public int TypeId { get; set; }
        [Required, Display(Name = "Make")]
        public int Make { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Record Date")]
        public DateTime RecordDate { get; set; }
        [Display(Name = "Available")]
        public bool isAvailable { get; set; }
        [Display(Name = "Picture")]
        public string PicturePath { get; set; }
        public HttpPostedFileBase Pictures { get; set; }
    }
}