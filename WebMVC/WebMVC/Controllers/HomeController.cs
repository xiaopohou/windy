using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Windy.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //return View("/admin/subexamplace");
            return View();
        }
        public ActionResult Default()
        {
            
            //return Redirect("subexamplace");
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
    }
}
