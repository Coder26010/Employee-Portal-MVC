using Employee_Portal_MVC.Models;
using System;
using System.Web.Mvc;


namespace Employee_Portal_MVC.Controllers
{
    //Controller => Employee
    public class EmployeeController : Controller
    {
        //Action Methods   => Employee/GetMessage
        //Action Methods must be Public
        //Action Methods must be non-static
        //Action Methods can not be overloaded like a c# Method
        //Action Methods are Http Verbs based that handle http request and response

        //[HttpGet] //This is GET Http verbs that handles only GET request
        //public string GetMessage()
        //{
        //    return "Hello from GetMessage Method";
        //}

        [HttpGet]
        public ViewResult Create()
        {
            return View("EmployeeCreate");
        }

        //[HttpPost]
        //public ViewResult Create(string EmpName,string EmailAddress,string Gender)
        //{
        //    return View("EmployeeCreate");
        //}

        [HttpPost]
        public ViewResult Create(EmployeeModel model)
        {
            return View("EmployeeCreate");
        }

    }
}