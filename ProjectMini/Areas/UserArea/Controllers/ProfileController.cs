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
    public class ProfileController : Controller
    {
        MiniProjectEntities entity = new MiniProjectEntities();
        // GET: UserArea/Profile
        public ActionResult Index()
        {
            var rec = this.entity.Users.Find(Session["UserId"]);
            return View(rec);

        }

        public ActionResult EditProfile()
        {
            var urec = this.entity.Users.Find(Session["UserId"]);
            return View(urec);
        }


        [HttpPost]
        public ActionResult EditProfile(User urec)
        {
            //var oldrec = this.entity.Users.Find(urec.UserId);

            /*oldrec.FirstName = urec.FirstName;
            oldrec.LastName = urec.LastName;
            oldrec.Address  = urec.Address;
            oldrec.MobileNo = urec.MobileNo;
            oldrec.EmailId = urec.EmailId;
            oldrec.Password = urec.Password;*/


            this.entity.Entry(urec).State = System.Data.Entity.EntityState.Modified;
            this.entity.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePass rec)
        {
            if (ModelState.IsValid)
            {
                var urecid = this.entity.Users.Find(Session["UserId"]);


                if (urecid != null)
                {
                    if (urecid.Password == rec.OldPassword)
                    {
                        if (rec.NewPassword == rec.ConfirmPassword)
                        {
                            urecid.Password = rec.NewPassword;
                            this.entity.Entry(urecid).State = System.Data.Entity.EntityState.Modified;
                            this.entity.SaveChanges();

                            return RedirectToAction("SignIn", "ManageUsers", new { area = "" });
                        }
                        else
                        {
                            ModelState.AddModelError("", "New Password and Confirm Password is not match");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Old Password is incorrect");
                    }
                }
            }
            return View(rec);
        }
    }
}