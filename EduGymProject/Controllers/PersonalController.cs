using EduGymProject.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using EduGymProject.ViewModels;

namespace EduGymProject.Controllers
{
    [Authorize(Roles = "A")]

    public class PersonalController : Controller
    {
        edugymEntities1 db = new edugymEntities1();

        public ActionResult Index()
        {
            var model = db.Personal.Include(x=>x.Departman).ToList();

            return View(model);
        }

        public ActionResult Yeni()
        {
            var model = new PersonalFormViewModels()
            {
                Departmanlar = db.Departman.ToList(),
                Personel = new Personal()
            };
            return View("PersonalForm", model);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Personal personel)
        {
            if (!ModelState.IsValid)
            {
                var model = new PersonalFormViewModels()
                {
                    Departmanlar = db.Departman.ToList(),
                    Personel = personel
                };
                return View("PersonalForm", model);
            }
            if (personel.id == 0) // Ekleme İşlemi
            {
                db.Personal.Add(personel);
            }
            else // Güncelleme
            {
                db.Entry(personel).State= System.Data.EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            var model = new PersonalFormViewModels()
            {
                Departmanlar = db.Departman.ToList(),
                Personel = db.Personal.Find(id)
            };

            return View("PersonalForm", model);
        }

        public ActionResult Sil(int id)
        {
            var silinecekPersonel = db.Personal.Find(id);
            if (silinecekPersonel == null)
            {
                return HttpNotFound();
            }
            db.Personal.Remove(silinecekPersonel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelleriListele(int id)
        {
            var model = db.Personal.Where(x => x.DepartmanId == id);
            return PartialView(model);
        }
        
    }
}