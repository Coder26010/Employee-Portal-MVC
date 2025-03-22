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
        [Required(ErrorMessage = "Name field is mandatory")]
        [RegularExpression("[a-zA-Z\\s]*", ErrorMessage = "Only characters are allowed")]
        [StringLength(50,ErrorMessage ="Maximum 50 characters are allowed")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Gender is mandatory to select")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Email field is mandatory")]
        [EmailAddress(ErrorMessage = "Invalid Email Id")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Mobile field is mandatory")]
        [RegularExpression("[6-9]{1}[0-9]{9}",ErrorMessage = "Inavlid mobile number")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Age field is mandatory")]
        [Range(18,49,ErrorMessage ="Kindly enter age between 18 and 49")]
        public int Age { get; set; }

        [ForeignKey("Department")]
        [Required(ErrorMessage = "Department is mandatory to select")]
        public int DepartmentId { get; set; }


        public string ProfileImagePath { get; set; }

        //Navigation Property
        public DepartmentEntity Department { get; set; } // one to one relationship


        //No Mapping with table
        [NotMapped]
        public List<DepartmentEntity> Departments { get; set; }

        //[NotMapped]
        //public HttpPostedFileBase ProfileImageFile { get; set; }
    }
}