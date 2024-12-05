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
    public class CategoryController : Controller
    {
        MiniProjectEntities entity = new MiniProjectEntities();
        // GET: UserArea/Category
        public ActionResult Index()
        {
            return View(this.entity.Categories.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.MainCategoryId = new SelectList(this.entity.MainCategories.ToList(), "MainCategoryId", "MainCategoryName");
            return View();
        }


        [HttpPost]
        public ActionResult Create(Category rec)
        {
            this.entity.Categories.Add(rec);
            this.entity.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(Int64 id)
        {
            var rec = this.entity.Categories.Find(id);

            ViewBag.MainCategoryId = new SelectList(this.entity.MainCategories.ToList(), "MainCategoryId", "MainCategoryName", rec.MainCategoryId);
            return View(rec);
        }


        [HttpPost]
        public ActionResult Edit(Category rec)
        {
            ViewBag.MainCategoryId = new SelectList(this.entity.MainCategories.ToList(), "MainCategoryId", "MainCategoryName");

            this.entity.Entry(rec).State = System.Data.Entity.EntityState.Modified;
            this.entity.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult Delete(Int64 id)
        {
            var rec = this.entity.Categories.Find(id);
            this.entity.Categories.Remove(rec);
            this.entity.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}