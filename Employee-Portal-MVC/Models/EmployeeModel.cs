using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employee_Portal_MVC.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
    }
}