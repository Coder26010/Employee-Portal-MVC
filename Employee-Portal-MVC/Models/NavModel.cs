using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace Employee_Portal_MVC.Models
{
    public class NavModel
    {
        public string Text { get; set; }

        public string Value { get; set; }

        public bool IsAuthorized { get; set; }
    }
}