using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Employee_Portal_MVC.ModelEntity
{
    [Table("TEMPLOYEE")]
    public class EmployeeEntity
    {
        [Key]
        [Column("EmpId")]
        public int Id { get; set;}

        [Column("Name")]
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }

        public int Age { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        //Navigation Property
        public DepartmentEntity Department { get; set; } // one to one relationship


        //No Mapping with table
        [NotMapped]
        public List<DepartmentEntity> Departments { get; set; }
    }
}