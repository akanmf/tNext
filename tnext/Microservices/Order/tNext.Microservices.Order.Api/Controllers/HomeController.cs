﻿using System.Web.Mvc;

namespace tNext.Microservices.Order.Api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
