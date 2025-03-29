using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Employee_Portal_MVC.ModelEntity
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext():base("name=EMPDB")
        {
            
        }
        public DbSet<DepartmentEntity> Departments { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }

        public DbSet<AppUsers> Appusers { get; set; }
    }
}