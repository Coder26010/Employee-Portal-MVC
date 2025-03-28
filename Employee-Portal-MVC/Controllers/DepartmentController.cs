﻿using Employee_Portal_MVC.ModelEntity;
using Employee_Portal_MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Employee_Portal_MVC.Controllers
{
   // [Authorize]
    public class DepartmentController : BaseController
    {
        private readonly EmployeeContext _employeeContext;
        private readonly DepartmentRepository repository;

        public DepartmentController()
        {
            _employeeContext = new EmployeeContext();
            repository = new DepartmentRepository();
        }

        public ActionResult ViewEmployee(int id)
        {
            TempData["DepartmentId"] = id;
            return RedirectToAction("Index", "Employee");
        }

        public ViewResult Index()
        {
            //var DepartmentList = _employeeContext.Departments.ToList();

            //List<DepartmentEntity> DepartmentList = new List<DepartmentEntity>();

            //string cs =  ConfigurationManager.ConnectionStrings["EMPDB"].ConnectionString;
            //using(SqlConnection con = new SqlConnection(cs))
            //{
            //    SqlCommand command = new SqlCommand("Select * from TDEPARTMENT", con);

            //    con.Open();
            //    SqlDataReader dataReader = command.ExecuteReader();
            //    if (dataReader.HasRows)
            //    {
            //        while (dataReader.Read())
            //        {
            //            DepartmentList.Add(new DepartmentEntity()
            //            {
            //                Id = Convert.ToInt32(dataReader["DepartmentId"]),
            //                DepartmentCode = dataReader["DepartmentCode"].ToString(),
            //                DepartmentName = dataReader["DepartmentName"].ToString()
            //            });
            //        }
            //    }

            //}

            var DepartmentList = repository.DepartmentList;

            return View(DepartmentList);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DepartmentModel entity)
        {
            string ErrorMessage = null;

            if (!ModelState.IsValid)
                return View(entity);

            if (_employeeContext.Departments.Any(x => x.DepartmentCode == entity.DepartmentCode))
            {
                ErrorMessage = "Record with Department Code " + entity.DepartmentCode + " already exists!";
            }
            else
            {
                _employeeContext.Departments.Add(new DepartmentEntity()
                {
                    Id = entity.Id,
                    DepartmentCode = entity.DepartmentCode,
                    DepartmentName = entity.DepartmentName
                });
                _employeeContext.SaveChanges();
                ModelState.Clear();
                string AlertMessage = "Record saved with department code " + entity.DepartmentCode + " successfully!";
                //TempData["Message"] = AlertMessage;
                //Serelize
                //TempData["Message"] = new JavaScriptSerializer().Serialize(new
                //{
                //    Title = "Record Saved",
                //    Type = "success",
                //    Message = AlertMessage
                //});
                ShowAlert(AlertType.success, "Record Saved", AlertMessage);
               

                return RedirectToAction(nameof(Index));
            }
            //TempData["Message"] = new JavaScriptSerializer().Serialize(new
            //{
            //    Title = "Record Saved",
            //    Type = "error",
            //    Message = ErrorMessage
            //});
            ShowAlert(AlertType.error, "Already Exists", ErrorMessage);
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
                    ShowAlert(AlertType.success, "Record Updated","Record updated for the department code " + entity.DepartmentCode);
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                ErrorMessage = "Record Not Found!";
            }

            //ViewBag.Message = ErrorMessage;
            ShowAlert(AlertType.error, "Not Found", ErrorMessage);
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
                ShowAlert(AlertType.success, "Record Deleted", "Record with department code " + department.DepartmentCode + " deleted permanenetly");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}