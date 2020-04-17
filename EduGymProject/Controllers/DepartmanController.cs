using EduGymProject.Models.EntityFramework;
using EduGymProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduGymProject.Controllers
{
    [Authorize(Roles = "A")]
    public class DepartmanController : Controller
    {
        // GET: Departman

        edugymEntities1 db = new edugymEntities1();

        
        public ActionResult Index()
        {
            var model = db.Departman.ToList();

            return View(model);
        }
        [HttpGet]
        public ActionResult Yeni()
        {

            return View("DepartmanForm", new Departman());
        }

        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Departman departman)
        {
            if (!ModelState.IsValid)
            {
                return View("DepartmanForm");
            }
            MesajViewModel model = new MesajViewModel();
            if (departman.id == 0)
            {
                db.Departman.Add(departman);
                model.Mesaj = departman.DepartmanAd + " Başarıyla Eklendi !";
            }
            else
            {
                var guncellenecekDepartman = db.Departman.Find(departman.id);
                if (guncellenecekDepartman == null)
                {
                    return HttpNotFound();
                }
                guncellenecekDepartman.DepartmanAd = departman.DepartmanAd;
                model.Mesaj = departman.DepartmanAd + " Başarıyla Güncellendi !";
            }

            db.SaveChanges();
            model.Status = true;
            model.LinkText = "Departman Listesi";
            model.Url = "/Departman";
            return View("_Mesaj", model);
        }

        public ActionResult Guncelle(int id)
        {
            var model = db.Departman.Find(id);
            if (model == null)
                return HttpNotFound();
            return View("DepartmanForm", model);
        }

        public ActionResult Sil(int id)
        {
            var silinecekDepartman = db.Departman.Find(id);
            if (silinecekDepartman == null)
                return HttpNotFound();
            db.Departman.Remove(silinecekDepartman);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}