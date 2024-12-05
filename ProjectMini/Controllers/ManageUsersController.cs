using ProjectMini.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ProjectMini.Controllers
{
    public class ManageUsersController : Controller
    {
        MiniProjectEntities entity = new MiniProjectEntities();
        // GET: ManageUsers
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(LoginVM rec)
        {
            if (ModelState.IsValid)
            {
                var urec = this.entity.Users.SingleOrDefault(p => p.EmailId == rec.EmailId
                           && p.Password == rec.Password);

                if (urec != null)
                {
                    Session["UserName"] = urec.FullName;
                    Session["UserId"] = urec.UserId;
                    return RedirectToAction("Index", "HomeUser", new { area = "UserArea" });
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Email Id and Password");
                    return View(rec);
                }
            }
            return View(rec);
        }


        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User rec)
        {
            if (ModelState.IsValid)
            {
                this.entity.Users.Add(rec);
                this.entity.SaveChanges();

                return RedirectToAction("SignIn");
            }

            return View(rec);
        }


        public ActionResult SignOut()
        {
            Session.Clear();
            Session.Abandon();
            return View();
        }
    }
}