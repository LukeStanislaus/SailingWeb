using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SailingWeb.Pages
{
    public class AlertController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.AlertMessage = "An alert";
            return View();
        }

    }
}