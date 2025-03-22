using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Employee_Portal_MVC.Models
{
    public class DepartmentModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Department Code is Mandatory")]
        [RegularExpression("[a-zA-Z0-9]{2,6}", ErrorMessage ="Enter a Valid code min 2 char or max 6 char")]
        public string DepartmentCode { get; set; }

        [Required(ErrorMessage = "Department Name is Mandatory")]
        public string DepartmentName { get; set; }
    }
}