using ProjectMini.CustFilter;
using ProjectMini.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectMini.Areas.UserArea.Controllers
{
    [UserAuth]
    public class MainCategoryController : Controller
    {
        MiniProjectEntities entity = new MiniProjectEntities();
        // GET: UserArea/MainCategory
        public ActionResult Index()
        {
            return View(this.entity.MainCategories.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MainCategory rec)
        {
            this.entity.MainCategories.Add(rec);
            this.entity.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(Int64 id)
        {
            var rec = this.entity.MainCategories.Find(id);
            return View(rec);
        }

        [HttpPost]
        public ActionResult Edit(MainCategory rec)
        {
            this.entity.Entry(rec).State = System.Data.Entity.EntityState.Modified;
            this.entity.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Delete(Int64 id)
        {
            var rec = this.entity.MainCategories.Find(id);
            this.entity.MainCategories.Remove(rec);
            this.entity.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}