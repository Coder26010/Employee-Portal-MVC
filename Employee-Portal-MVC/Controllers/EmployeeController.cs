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
        private readonly string[] AllowedFileType = { ".png", ".jpg", ".jpeg" };
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
        public ActionResult Create(EmployeeEntity employee, HttpPostedFileBase ProfileImage)
        {
            string errorMessage = null;

            if (ModelState.IsValid)
            {
                if (ProfileImage != null)
                {
                    string fileExtension = System.IO.Path.GetExtension(ProfileImage.FileName).ToLower();
                    if (!AllowedFileType.Any(x => x == fileExtension))
                        errorMessage = "Allowed file types ares '" + string.Join(",", AllowedFileType) + "'";
                    else if (ProfileImage.ContentLength > (1024 * 1024 * 2))
                        errorMessage = "You can not upload a file greater than 2 MB";
                    else
                    {
                        string uniquiValue = DateTime.Now.ToString("ddmmyyyyHHmmss") + Guid.NewGuid().ToString();
                        string UploadFolderPath = Server.MapPath("~/ProfileImages");
                        string UploadFilePath = System.IO.Path.Combine(UploadFolderPath, (uniquiValue + ProfileImage.FileName));
                        ProfileImage.SaveAs(UploadFilePath);
                        employee.ProfileImagePath = "~/ProfileImages/" + (uniquiValue + ProfileImage.FileName);
                    }
                }
                else
                {
                    errorMessage = "Please select image file for profile photo";
                }


                if (errorMessage != null)
                {
                    ShowAlert(AlertType.error, "Error", errorMessage);
                    employee.Departments = _employeeContext.Departments.ToList();
                    return View(employee);
                }

                if (_employeeContext.Employees.Any(x => x.EmailAddress == employee.EmailAddress || x.MobileNo == employee.MobileNo))
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

                if (errorMessage != null)
                {
                    ShowAlert(AlertType.warning, "Alredy Exists", errorMessage);
                }
                employee.Departments = _employeeContext.Departments.ToList();
                return View(employee);
            }

            employee.Departments = _employeeContext.Departments.ToList();
            return View(employee);
        }
    }
}