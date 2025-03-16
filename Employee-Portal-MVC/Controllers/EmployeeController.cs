using Employee_Portal_MVC.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employee_Portal_MVC.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly EmployeeContext _employeeContext;
        public EmployeeController()
        {
            _employeeContext = new EmployeeContext();
        }

        [HttpGet]
        public ViewResult Index()
        {
            List<EmployeeEntity> employees = _employeeContext.Employees.ToList();
            return View(employees);
        }

        [HttpGet]
        public ActionResult Create()
        {
            //List<DepartmentEntity> departments = _employeeContext.Departments.ToList();
            EmployeeEntity employee = new EmployeeEntity()
            {
                Departments = _employeeContext.Departments.ToList()
            };
            return View(employee);
        }

        [HttpPost]
        public ActionResult Create(EmployeeEntity employee)
        {
            string errorMessage = null;
            if(_employeeContext.Employees.Any(x => x.EmailAddress == employee.EmailAddress || x.MobileNo == employee.MobileNo))
            {
                errorMessage = "Employee already registered with given email or mobile number.";
            }
            else
            {
                _employeeContext.Employees.Add(employee);
                _employeeContext.SaveChanges();
                ShowAlert(AlertType.success, "Record Added", "Record save successfully!");
                return RedirectToAction(nameof(Index));
            }

            if(errorMessage != null)
            {
                ShowAlert(AlertType.warning, "Already Exists", errorMessage);
            }
            employee.Departments = _employeeContext.Departments.ToList();
            return View(employee);
        }
    }
}