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
    public class ProductController : Controller
    {
        MiniProjectEntities entity = new MiniProjectEntities();
        // GET: UserArea/Product
        public ActionResult Index()
        {
            ViewBag.MainCategoryId = new SelectList(this.entity.MainCategories.ToList(), "MainCategoryId", "MainCategoryName");
            ViewBag.CategoryId = new SelectList(this.entity.Categories.ToList(), "CategoryId", "CategoryName");
            
            
            return View(this.entity.Products.ToList());
        }

        public JsonResult GetCategories(Int64? maincatid)
        {
            var categories = this.entity.Categories
                .Where(p => p.MainCategoryId == maincatid)  
                .Select(p => new CategoryVM
                {
                    CategoryId = p.CategoryId,
                    CategoryName = p.CategoryName
                }).ToList();

            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchByCategory(Int64? MainCategoryId = 0, Int64? CategoryId = 0)
        {
            ViewBag.MainCategoryId = new SelectList(this.entity.MainCategories.ToList(), "MainCategoryId", "MainCategoryName", CategoryId);
            ViewBag.CategoryId = new SelectList(this.entity.Categories.ToList(), "CategoryId", "CategoryName");

            var prod = this.entity.Products.ToList();

            if (MainCategoryId != 0)
            {
                prod = this.entity.Products.Where(p => p.Category.MainCategoryId == MainCategoryId).ToList();

            }
            if (CategoryId != 0)
            {
                prod = this.entity.Products.Where(p => p.CategoryId == CategoryId).ToList();
            }


            return View("Index", prod.ToList());

        }

        public ActionResult Create()
        {
            ViewBag.MfgId = new SelectList(this.entity.Mfgs.ToList(), "MfgId", "MfgName");
            ViewBag.CategoryId = new SelectList(this.entity.Categories.ToList(), "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product rec)
        {
            this.entity.Products.Add(rec);
            this.entity.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult Edit(Int64 id)
        {
            var rec = this.entity.Products.Find(id);

            ViewBag.MfgId = new SelectList(this.entity.Mfgs.ToList(), "MfgId", "MfgName", rec.MfgId);
            ViewBag.CategoryId = new SelectList(this.entity.Categories.ToList(), "CategoryId", "CategoryName", rec.CategoryId);

            return View(rec);
        }


        [HttpPost]
        public ActionResult Edit(Product rec)
        {
            this.entity.Entry(rec).State = System.Data.Entity.EntityState.Modified;
            this.entity.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult Delete(Int64 id)
        {
            var rec = this.entity.Products.Find(id);
            this.entity.Products.Remove(rec);
            this.entity.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}