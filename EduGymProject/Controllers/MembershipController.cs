using EduGymProject.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduGymProject.Controllers
{
    [Authorize(Roles = "A")]
    public class MembershipController : Controller
    {
        
        edugymEntities1 db = new edugymEntities1();
        // GET: Membership
        public ActionResult Index()
        {
            var model = db.User.ToList();

            return View(model);
        }

        public ActionResult Sil(int id)
        {
            var silinecekUser = db.User.Find(id);
            if (silinecekUser == null)
                return HttpNotFound();
            db.User.Remove(silinecekUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}