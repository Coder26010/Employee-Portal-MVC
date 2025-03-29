using Employee_Portal_MVC.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Employee_Portal_MVC.Controllers
{
    public class AccountController : BaseController
    {
        private readonly EmployeeContext _context = new EmployeeContext();

        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AppUsers appUsers)
        {
            if (_context.Appusers.Any(x => x.EmailAddress.ToLower() == appUsers.EmailAddress.ToLower() && x.Password == appUsers.Password))
            {

                //Make user Login;

                //Create A authentication ticket

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, appUsers.EmailAddress, 
                                                                                    DateTime.Now,
                                                                                    DateTime.Now.AddMinutes(30),
                                                                                    false,"");
                //Encrypt the ticket for security
                string encryptTicket = FormsAuthentication.Encrypt(ticket);


                //add the ticket into user browser as a cookie
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket);
                cookie.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Add(cookie);

                return RedirectToAction("Index", "Department");
            }
            ShowAlert(AlertType.error, "Error", "Invalid Email Id or Password!");
            return View();
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(AppUsers appUsers)
        {
            if (ModelState.IsValid)
            {
                if (!_context.Appusers.Any(x => x.EmailAddress.ToLower() == appUsers.EmailAddress.ToLower()))
                {
                    _context.Appusers.Add(appUsers);
                    _context.SaveChanges();
                    return RedirectToAction("Login");
                }
                ShowAlert(AlertType.info, "Info", "User already register with email id - " + appUsers.EmailAddress);
                return View();
            }
            else
            {
                ShowAlert(AlertType.error, "Error", "Invalid data.");
                return View();
            }
        }
    }
}