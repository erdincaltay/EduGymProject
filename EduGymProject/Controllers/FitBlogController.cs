using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduGymProject.Controllers
{
    [AllowAnonymous]
    public class FitBlogController : Controller
    {
        // GET: FitBlog
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Fitness()
        {
            return View();
        }

        public ActionResult Food()
        {
            return View();
        }
        public ActionResult Live()
        {
            return View();
        }
        public ActionResult Office()
        {
            return View();
        }
    }
}