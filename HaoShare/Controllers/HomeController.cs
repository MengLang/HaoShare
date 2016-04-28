using HaoShare.Core.Business;
using HaoShare.Models.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaoShare.Controllers
{

    public class HomeController : Controller
    {
        [CheckUserStatusFilter]
        public ActionResult Index()
        {
            if (Session["UserInfo"] == null)
                ViewBag.Name = "匿名";
            else
                ViewBag.Name = "无名";
            return View();
        }
        public ActionResult Demo()
        {
            return View();
        }
        public ActionResult Test()
        {
#if DEBUG
             LogCore.LogSave("Messs", "123123", "20");
#endif
           
            return Content("success");
        }
    }
}