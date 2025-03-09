using Employee_Portal_MVC.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employee_Portal_MVC.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly EmployeeContext _employeeContext;

        public DepartmentController()
        {
            _employeeContext = new EmployeeContext();
        }

        public ViewResult Index()
        {
            var DepartmentList = _employeeContext.Departments.ToList();
            return View(DepartmentList);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public RedirectToRouteResult Create(DepartmentEntity entity)
        {
            _employeeContext.Departments.Add(entity);
            _employeeContext.SaveChanges();
            ModelState.Clear();
            return RedirectToAction(nameof(Index));
        }
    }
}