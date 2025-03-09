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
        public ActionResult Create(DepartmentEntity entity)
        {
            string ErrorMessage = null;

            if (_employeeContext.Departments.Any(x => x.DepartmentCode == entity.DepartmentCode))
            {
                ErrorMessage = "Record with Department Code " + entity.DepartmentCode + " already exists!";
            }
            else
            {
                _employeeContext.Departments.Add(entity);
                _employeeContext.SaveChanges();
                ModelState.Clear();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Message = ErrorMessage;
            return View(entity);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            DepartmentEntity department = _employeeContext.Departments.Find(id);
            if (department != null)
                return View(department);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult Edit(DepartmentEntity entity)
        {
            string ErrorMessage = null;
            DepartmentEntity department = _employeeContext.Departments.Find(entity.Id);
            if (department != null)
            {

                if(_employeeContext.Departments.Any(x => x.DepartmentCode == entity.DepartmentCode && x.Id != entity.Id))
                {
                    ErrorMessage = "Record with Department Code " + entity.DepartmentCode + " already exists!";
                }
                else
                {
                    department.DepartmentName = entity.DepartmentName;
                    department.DepartmentCode = entity.DepartmentCode;
                    _employeeContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                ErrorMessage = "Record Not Found!";
            }

            ViewBag.Message = ErrorMessage;
            return View(entity);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            DepartmentEntity department = _employeeContext.Departments.Find(id);
            if (department != null)
            {
                _employeeContext.Departments.Remove(department);
                _employeeContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}