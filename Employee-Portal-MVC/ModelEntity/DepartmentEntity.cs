using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Employee_Portal_MVC.ModelEntity
{
    [Table("TDEPARTMENT")]
    public class DepartmentEntity
    {
        [Key]
        [Column("DepartmentId")]
        public int Id { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }

        //Navigation property
        public ICollection<EmployeeEntity> Employees { get; set; } // one to many relationship
    }
}