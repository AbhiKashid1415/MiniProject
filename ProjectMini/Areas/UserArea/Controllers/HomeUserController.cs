using ProjectMini.CustFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectMini.Areas.UserArea.Controllers
{
    [UserAuth]
    public class HomeUserController : Controller
    {
        // GET: UserArea/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}