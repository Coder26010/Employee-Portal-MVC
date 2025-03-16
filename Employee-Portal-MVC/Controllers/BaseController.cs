using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Employee_Portal_MVC.Controllers
{
    public enum AlertType
    {
        success,error,warning,info
    }
    public class BaseController : Controller
    {
        // GET: Base
       public void ShowAlert(AlertType alertType,string title,string message)
        {
            TempData["Message"] = new JavaScriptSerializer().Serialize(new
            {
                Title = title,
                Type = alertType.ToString(),
                Message = message
            });
        }
    }
}